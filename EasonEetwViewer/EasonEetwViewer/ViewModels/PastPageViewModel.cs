using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using EasonEetwViewer.Dmdata.Api.Abstractions;
using EasonEetwViewer.Dmdata.Api.Dtos.Record.EarthquakeParameter;
using EasonEetwViewer.Dmdata.Api.Dtos.Record.GdEarthquake;
using EasonEetwViewer.Dmdata.Api.Dtos.Response;
using EasonEetwViewer.Dmdata.Authentication.Abstractions;
using EasonEetwViewer.Dmdata.Authentication.Events;
using EasonEetwViewer.Dmdata.Dtos.DmdataComponent;
using EasonEetwViewer.Dmdata.Dtos.Enum;
using EasonEetwViewer.Dmdata.Telegram.Abstractions;
using EasonEetwViewer.Dmdata.Telegram.Dtos.EarthquakeInformation;
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
    /// <param name="telegramParser">The telegram parser to be used.</param>
    /// <param name="timeProvider">The time provider to be used.</param>
    /// <param name="logger">The logger to be used.</param>
    public PastPageViewModel(
        MapResourcesProvider resources,
        IAuthenticationHelper authenticatorWrapper,
        IApiCaller apiCaller,
        ITelegramRetriever telegramRetriever,
        ITelegramParser telegramParser,
        ITimeProvider timeProvider,
        ILogger<PastPageViewModel> logger) : base(
            resources,
            authenticatorWrapper,
            apiCaller,
            telegramRetriever,
            telegramParser,
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
    private const string _observationPointLayerName = "Point";
    private const string _hypocentreLayerName = "Hypocentre";
    private CancellationTokenSource _cts = new();
    async partial void OnSelectedEarthquakeChanged(EarthquakeItemTemplate? value)
    {
        CancellationToken token = ResetDetails();

        if (value is null)
        {
            return;
        }

        GdEarthquakeEvent? rsp = await _apiCaller.GetPathEarthquakeEventAsync(value.EventId);
        IEnumerable<TelegramItem> telegrams = rsp?.EarthquakeEvent.Telegrams ?? [];
        TelegramItem? telegram = telegrams
            .Where(x
                => _telegramParser.ParseSchemaInformation(x.SchemaVersion) == typeof(EarthquakeInformationSchema))
            .Where(x
                => x.TelegramHead.Type is "VXSE51" or "VXSE52" or "VXSE53")
            .GroupBy(x
                => x.TelegramHead.Type)
            .Select(x
                => x.OrderByDescending(y
                    => y.Serial)
                    .First())
            .OrderByDescending(x
                => x.TelegramHead.Type)
            .FirstOrDefault();

        EarthquakeInformationSchema? telegramInfo = telegram is not null
            ? await _telegramRetriever.GetJsonTelegramAsync(telegram.Id) as EarthquakeInformationSchema
            : null;
        IEnumerable<DetailIntensityTemplate>? tree = null;
        MRect? regionLimits = null;

        if (telegramInfo?.Body.Intensity is not null)
        {
            await LoadEarthquakeObservationStations();
            (ILayer stationLayer, ILayer regionLayer, regionLimits, tree) = await DealWithTelegramIntensities(telegramInfo.Body.Intensity);
            if (!token.IsCancellationRequested)
            {
                Map.Layers.Add(new RasterizingLayer(regionLayer));
                Map.Layers.Add(new RasterizingLayer(stationLayer));
            }
        }

        if (!token.IsCancellationRequested)
        {
            EarthquakeDetails = new(value, telegramInfo, tree);
        }

        // Mark Hypocentre
        MRect? hypocentreLimits = null;
        CoordinateComponent? coordinate = telegramInfo?.Body.Earthquake?.Hypocentre.Coordinates ?? value.Hypocentre?.Coordinates;
        if (coordinate?.Longitude is not null &&
            coordinate?.Latitude is not null)
        {
            MPoint convertedCoordinates = SphericalMercator
                .FromLonLat(
                coordinate.Longitude.DoubleValue,
                coordinate.Latitude.DoubleValue)
                .ToMPoint();
            hypocentreLimits = convertedCoordinates.MRect;

            ILayer layer = new MemoryLayer()
            {
                Name = _hypocentreLayerName,
                Features = [new PointFeature(convertedCoordinates)],
                Style = _resources.HypocentreShapeStyle
            };

            if (!token.IsCancellationRequested)
            {
                Map.Layers.Add(layer);
            }
        }

        // Zoom to Box
        MRect limits = regionLimits ?? _mainLimitsOfJapan;
        limits = limits.Join(hypocentreLimits);

        if (regionLimits is not null || hypocentreLimits is not null)
        {
            limits = limits.Multiply(_extendFactor);
        }

        if (!token.IsCancellationRequested)
        {
            Map.Navigator.ZoomToBox(limits);
        }
    }
    private async Task<(ILayer, ILayer, MRect, IEnumerable<DetailIntensityTemplate>)> DealWithTelegramIntensities(IntensityDetails intensityDetails)
    {
        // Join the station data with the stations retrieved from the API
        IEnumerable<(Station station, Intensity intensity)> stationData = intensityDetails
            .Stations
            .Where(x
                => x.MaxInt?.ToIntensity() is Intensity)
            .Join(
                _earthquakeObservationStations!,
                si => si.Code,
                s => s.XmlCode,
            (si, s)
                => (s, (Intensity)si.MaxInt?.ToIntensity()!));

        return (
            CreateStationLayer(stationData),
            CreateRegionLayer(intensityDetails.Regions),
            await CreateRegionLimits(intensityDetails),
            CreateIntensityTree(stationData));
    }
    private async Task<MRect> CreateRegionLimits(IntensityDetails intensityDetails)
        => (await _resources.Region.GetFeaturesAsync(new(new MSection(_limitsOfJapan, 1))))
            .Join(intensityDetails.Regions,
            f => (string)f["code"]!,
            r => r.Code,
            (f, r) => f.Extent)
            .Where(regionLimits => regionLimits is not null)
            .Select(regionLimits => regionLimits!)
            .Aggregate((region1, region2) => region1.Join(region2));
    private static PointFeature CreateStationFeature(Station station, Intensity intensity)
        => new(
            SphericalMercator.FromLonLat(
                station.Longitude,
                station.Latitude)
            .ToMPoint())
        {
            Styles = [CreateStationStyle(intensity)]
        };
    private static SymbolStyle CreateStationStyle(Intensity intensity)
        => new()
        {
            SymbolScale = 0.25,
            Fill = new Brush(intensity.ToColourString().ToColour()),
            Outline = new Pen { Color = Color.Black }
        };
    private static MemoryLayer CreateStationLayer(IEnumerable<(Station station, Intensity intensity)> stationData)
        => new()
        {
            Name = _observationPointLayerName,
            Features = stationData
                .Select(x => CreateStationFeature(x.station, x.intensity)),
            Style = null
        };
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
            RegionIntensity? region = regions.SingleOrDefault(r
                => r.Code == (string)f["code"]!);

            return region?.MaxInt is Intensity intensity
                ? new VectorStyle()
                {
                    Fill = new Brush(Color.Opacity(Color.FromString(intensity.ToColourString()), 0.60f))
                }
                : null;
        });
    private IEnumerable<DetailIntensityTemplate> CreateIntensityTree(IEnumerable<(Station station, Intensity intensity)> stationData)
        => stationData
            .Join(_resources.Prefectures,
                x
                    => x.station.City.Code[0..2],
                prefecture
                    => prefecture.Code,
                (si, prefecture)
                => (prefecture, si.station, si.intensity))
            .GroupBy(x => x.intensity)
            .Select(x => new DetailIntensityTemplate()
            {
                Intensity = x.Key,
                PositionNodes = x
                    .GroupBy(y => y.prefecture)
                    .Select(y => new DisplayNode()
                    {
                        Name = y.Key.Name,
                        SubNodes = y
                            .GroupBy(z => z.station.Region)
                            .Select(z => new DisplayNode()
                            {
                                Name = z.Key.KanjiName,
                                SubNodes = z
                                    .GroupBy(a => a.station.City)
                                    .Select(a => new DisplayNode()
                                    {
                                        Name = a.Key.KanjiName,
                                        SubNodes = a
                                            .Select(a => new DisplayNode()
                                            {
                                                Name = a.station.KanjiName
                                            })
                                    })
                            })
                    })
            })
            .OrderByDescending(x => x.Intensity);
    private CancellationToken ResetDetails()
    {
        _ = Map.Layers.Remove(x
            => x.Name is _regionLayerName);
        _ = Map.Layers.Remove(x
            => x.Name is _observationPointLayerName);
        _ = Map.Layers.Remove(x
            => x.Name is _hypocentreLayerName);
        Map.Navigator.ZoomToBox(_mainLimitsOfJapan);

        _cts.Cancel();
        _cts.Dispose();
        _cts = new CancellationTokenSource();
        CancellationToken token = _cts.Token;
        EarthquakeDetails = null;
        return token;
    }

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
