using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using EasonEetwViewer.Dmdata.Api.Abstractions;
using EasonEetwViewer.Dmdata.Api.Dtos.Record.EarthquakeParameter;
using EasonEetwViewer.Dmdata.Api.Dtos.Record.GdEarthquake;
using EasonEetwViewer.Dmdata.Api.Dtos.Response;
using EasonEetwViewer.Dmdata.Authentication.Abstractions;
using EasonEetwViewer.Dmdata.Authentication.Events;
using EasonEetwViewer.Dmdata.Dtos.Enum;
using EasonEetwViewer.Dmdata.Telegram.Abstractions;
using EasonEetwViewer.Dmdata.Telegram.Dtos.EarthquakeInformation;
using EasonEetwViewer.Dmdata.Telegram.Dtos.EarthquakeInformation.Enum;
using EasonEetwViewer.Dmdata.Telegram.Dtos.Schema;
using EasonEetwViewer.Extensions;
using EasonEetwViewer.Models.PastPage;
using EasonEetwViewer.Services;
using EasonEetwViewer.Services.TimeProvider;
using EasonEetwViewer.ViewModels.ViewModelBases;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Projections;
using Mapsui.Styles;
using Mapsui.Styles.Thematics;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.ViewModels;
/// <summary>
/// The view model for the past page.
/// </summary>
internal partial class PastPageViewModel : MapViewModelBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PastPageViewModel"/> class.
    /// </summary>
    /// <param name="resources">The map resources to be used.</param>
    /// <param name="authenticatorWrapper">The authenticator to be used.</param>
    /// <param name="apiCaller">The API caller to be used.</param>
    /// <param name="telegramRetriever">The telegram retriever to be used.</param>
    /// <param name="timeProvider">The time provider to be used.</param>
    /// <param name="logger">The logger to be used.</param>
    public PastPageViewModel(
        MapResourcesProvider resources,
        IAuthenticationHelper authenticatorWrapper,
        IApiCaller apiCaller,
        ITelegramRetriever telegramRetriever,
        ITimeProvider timeProvider,
        ILogger<PastPageViewModel> logger) : base(
            resources,
            authenticatorWrapper,
            apiCaller,
            telegramRetriever,
            timeProvider,
            logger)
    {
        _logger = logger;

        if (authenticatorWrapper.AuthenticationStatus is not AuthenticationStatus.Null)
        {
            Task.Run(LoadEarthquakeObservationStations).Wait();
        }

        authenticatorWrapper.StatusChanged += AuthenticatorWrapperStatusChangedEventHandler;
    }

    /// <summary>
    /// The logger to be used.
    /// </summary>
    private readonly ILogger<PastPageViewModel> _logger;

    #region earthquakeDetails
    /// <summary>
    /// The earthquake details for the currently selected earthquake, displayed on the left.
    /// </summary>
    [ObservableProperty]
    private EarthquakeDetailsTemplate? _earthquakeDetails;

    private const double _extendFactor = 1.2;

    private const string _regionLayerName = "Regions";
    private const string _obsPointLayerName = "Point";
    private const string _hypocentreLayerName = "Hypocentre";
    private CancellationTokenSource _cts = new();

    async partial void OnSelectedEarthquakeChanged(EarthquakeItemTemplate? value)
    {
        _ = Map.Layers.Remove(x => x.Name == _regionLayerName);
        _ = Map.Layers.Remove(x => x.Name == _obsPointLayerName);
        _ = Map.Layers.Remove(x => x.Name == _hypocentreLayerName);

        Map.Navigator.ZoomToBox(_mainLimitsOfJapan);

        // async code needs cancellation token to prevent different ones add layers
        _cts.Cancel();
        _cts.Dispose();
        _cts = new CancellationTokenSource();
        CancellationToken token = _cts.Token;

        EarthquakeDetails = null;

        if (value is null)
        {
            return;
        }

        MRect? regionLimits = null;

        GdEarthquakeEvent? rsp = await _apiCaller.GetPathEarthquakeEventAsync(value.EventId);
        IEnumerable<TelegramItem> telegrams = rsp?.EarthquakeEvent.Telegrams ?? [];
        telegrams = telegrams .Where(x => x.TelegramHead.Type is "VXSE53");
        if (telegrams.Count() != 0)
        {
            await LoadEarthquakeObservationStations();

            TelegramItem telegram = telegrams.MaxBy(x => x.Serial)!;
            EarthquakeInformationSchema? telegramInfo = await _telegramRetriever.GetJsonTelegramAsync(telegram.Id) as EarthquakeInformationSchema;
            IntensityDetailTree tree = new();

            if (telegramInfo is not null && telegramInfo.Body.Intensity is not null)
            {
                IEnumerable<(Station s, Intensity i)> stationData = telegramInfo.Body.Intensity.Stations
                    .Where(x => (x.MaxInt is IntensityWithUnreceived newInt && newInt.ToEarthquakeIntensity() is Intensity))
                    .Join(_earthquakeObservationStations!,
                    si => si.Code,
                    s => s.XmlCode,
                    (si, s) => (s, (Intensity)((IntensityWithUnreceived)si.MaxInt!).ToEarthquakeIntensity()!));

                IEnumerable<IFeature> stationFeatures = stationData
                    .Select(x =>
                        new PointFeature(SphericalMercator.FromLonLat(x.s.Longitude, x.s.Latitude).ToMPoint())
                        {
                            Styles = [new SymbolStyle()
                            {
                                SymbolScale = 0.25,
                                Fill = new Brush(Color.FromString(x.i.ToColourString())),
                                Outline = new Pen { Color = Color.Black }
                            }]
                        });

                IEnumerable<(Intensity, PositionNode)> intensityAndNodes = stationData
                    .Join(_resources.Prefectures,
                        x => x.s.City.Code[0..2],
                        p => p.Code,
                        (si, p) => (p, si.s, si.i))
                    .Select(x =>
                        (x.i, new PositionNode(x.p.Name, x.p.Code,
                                new(x.s.Region.KanjiName, x.s.Region.Code,
                                    new(x.s.City.KanjiName, x.s.City.Code,
                                        new(x.s.KanjiName, x.s.XmlCode))))));

                tree.AddPositionNode(intensityAndNodes);

                ILayer layer = new MemoryLayer()
                {
                    Name = _obsPointLayerName,
                    Features = stationFeatures,
                    Style = null
                };

                if (!token.IsCancellationRequested)
                {
                    Map.Layers.Add(new RasterizingLayer(CreateRegionLayer(telegramInfo.Body.Intensity.Regions!)));
                    Map.Layers.Add(new RasterizingLayer(layer));
                }

                IEnumerable<MRect?> regions = (await _resources.Region.GetFeaturesAsync(new(new MSection(_limitsOfJapan, 1))))
                    .Join(telegramInfo.Body.Intensity.Regions,
                    f => f["code"]?.ToString()?.ToLower(),
                    r => r.Code,
                    (f, r) => (f.Extent));

                foreach (MRect? region in regions)
                {
                    regionLimits = regionLimits is null ? region : regionLimits.Join(region);
                }
            }

            string informationalText = telegramInfo is null ? string.Empty : ToInformationString(telegramInfo);

            if (!token.IsCancellationRequested)
            {
                EarthquakeDetails = new()
                {
                    EventId = value.EventId,
                    Intensity = value.Intensity,
                    OriginTime = value.OriginTime,
                    Hypocentre = value.Hypocentre,
                    Magnitude = value.Magnitude,
                    InformationalText = informationalText,
                    LastUpdated = telegramInfo?.ReportDateTime,
                    DetailDisplay = tree.ToItemControlDisplay()
                };
            }
        }

        // Mark Hypocentre
        MRect? hypocentreLimits = null;
        if (value.Hypocentre is not null
            && value.Hypocentre.Coordinates.Longitude is not null
            && value.Hypocentre.Coordinates.Latitude is not null)
        {
            MPoint coords = SphericalMercator.FromLonLat(value.Hypocentre.Coordinates.Longitude.DoubleValue, value.Hypocentre.Coordinates.Latitude.DoubleValue).ToMPoint();
            hypocentreLimits = coords.MRect;

            ILayer layer = new MemoryLayer()
            {
                Name = _hypocentreLayerName,
                Features = [new PointFeature(coords)],
                Style = _resources.HypocentreShapeStyle
            };

            if (!token.IsCancellationRequested)
            {
                Map.Layers.Add(layer);
            }
        }

        if (!token.IsCancellationRequested)
        {
            MRect limits = regionLimits is null ? _mainLimitsOfJapan : regionLimits;
            limits = hypocentreLimits is null ? limits : limits.Join(hypocentreLimits);

            if (regionLimits is not null || hypocentreLimits is not null)
            {
                limits = limits.Multiply(_extendFactor);
            }

            Map.Navigator.ZoomToBox(limits);
        }
    }

    private static string ToInformationString(EarthquakeInformationSchema earthquake)
    {
        StringBuilder sb = new();

        if (earthquake.Headline is not null)
        {
            _ = sb.AppendLine(earthquake.Headline);
        }

        if (earthquake.Body.Text is not null)
        {
            _ = sb.AppendLine(earthquake.Body.Text);
        }

        if (earthquake.Body.Comments is not null)
        {
            if (earthquake.Body.Comments.FreeText is not null)
            {
                _ = sb.AppendLine(earthquake.Body.Comments.FreeText);
            }

            if (earthquake.Body.Comments.Forecast is not null)
            {
                _ = sb.AppendLine(earthquake.Body.Comments.Forecast.Text);
            }

            if (earthquake.Body.Comments.Var is not null)
            {
                _ = sb.AppendLine(earthquake.Body.Comments.Var.Text);
            }
        }

        return sb.ToString();
    }

    private Layer CreateRegionLayer(IEnumerable<RegionIntensity> regions)
        => new()
        {
            Name = _regionLayerName,
            DataSource = _resources.Region,
            Style = CreateRegionThemeStyle(regions)
        };

    // Adapted from https://mapsui.com/v5/samples/ - Styles - ThemeStyle on ShapeFile
    private static ThemeStyle CreateRegionThemeStyle(IEnumerable<RegionIntensity> regions)
        => new(f =>
            {
                RegionIntensity? region = regions.FirstOrDefault(r => r.Code == f["code"]?.ToString()?.ToLower());
                return region is null || region.MaxInt is null
                    ? null
                    : new VectorStyle()
                    {
                        Fill = new Brush(Color.Opacity(Color.FromString(((Intensity)region.MaxInt).ToColourString()), 0.60f))
                    };
            });

    /// <summary>
    /// The address for the Yahoo webpage for earthquakes.
    /// </summary>
    private const string _yahooWebpageAddress = "https://typhoon.yahoo.co.jp/weather/jp/earthquake/{0}.html";

    /// <summary>
    /// Jumps to the Yahoo webpage for the currently selected earthquake.
    /// </summary>
    [RelayCommand]
    private void JumpYahooWebpage()
    {
        if (EarthquakeDetails is null)
        {
            return;
        }

        _ = Process.Start(new ProcessStartInfo // https://stackoverflow.com/a/61035650/
        {
            FileName = string.Format(
                _yahooWebpageAddress,
                EarthquakeDetails.EventId),
            UseShellExecute = true
        });
    }
#endregion

    #region earthquakeObservationStations
    /// <summary>
    /// The list of earthquake observation stations.
    /// </summary>
    private IEnumerable<Station>? _earthquakeObservationStations = null;
    /// <summary>
    /// Whether the earthquake observation stations have been retrieved.
    /// </summary>
    private bool IsStationsRetrieved
        => _earthquakeObservationStations is not null;
    /// <summary>
    /// Loads the earthquake observation stations.
    /// </summary>
    /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
    private async Task LoadEarthquakeObservationStations()
    {
        if (IsStationsRetrieved)
        {
            return;
        }

        EarthquakeParameter? rsp = await _apiCaller.GetEarthquakeParameterAsync();
        _earthquakeObservationStations = rsp?.ItemList;
    }
    /// <summary>
    /// Event handler for the authenticator status changed event.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The event arguments.</param>
    private async void AuthenticatorWrapperStatusChangedEventHandler(object? sender, AuthenticationStatusChangedEventArgs e)
        => await LoadEarthquakeObservationStations();
    #endregion

    #region loadEarthquakeAction
    /// <summary>
    /// The list of earthquakes on the sidebar.
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<EarthquakeItemTemplate> _earthquakeList = [];
    /// <summary>
    /// The current selected earthquake.
    /// </summary>
    [ObservableProperty]
    private EarthquakeItemTemplate? _selectedEarthquake;
    /// <summary>
    /// The cursor token for the earthquake list.
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsLoadExtraEnabled))]
    private string? _cursorToken = null;

    /// <summary>
    /// Whether the load extra button is enabled.
    /// </summary>
    public bool IsLoadExtraEnabled
        => CursorToken is not null;

    /// <summary>
    /// Refreshes the earthquake list, and clears existing earthquakes and the selected earthquake.
    /// </summary>
    /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
    [RelayCommand]
    private async Task RefreshEarthquakeList()
    {
        GdEarthquakeList? rsp = await _apiCaller.GetPastEarthquakeListAsync(limit: 50);
        IEnumerable<EarthquakeInfo> eqList = rsp?.ItemList ?? [];

        CursorToken = rsp?.NextToken ?? null;
        SelectedEarthquake = null;
        EarthquakeList.Clear();
        EarthquakeList.AddRange(eqList.Select(x
            => new EarthquakeItemTemplate(x)));
    }

    /// <summary>
    /// Loads 10 extra earthquakes to the list using the cursor token.
    /// </summary>
    /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
    [RelayCommand]
    private async Task LoadExtraEarthquakes()
    {
        if (!IsLoadExtraEnabled)
        {
            return;
        }

        GdEarthquakeList? rsp = await _apiCaller.GetPastEarthquakeListAsync(limit: 10, cursorToken: CursorToken);
        IEnumerable<EarthquakeInfo> eqList = rsp?.ItemList ?? [];
        CursorToken = rsp?.NextToken;
        EarthquakeList.AddRange(eqList.Select(x
            => new EarthquakeItemTemplate(x)));
    }
    #endregion
}
