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
            Task.Run(LoadEarthquakeObservationStationsAsync).Wait();
        }

        authenticatorWrapper.StatusChanged += AuthenticatorWrapperStatusChangedEventHandler;

        _hypocentreLayer = new()
        {
            Style = _resources.HypocentreShapeStyle
        };
        _regionLayer = new()
        {
            DataSource = _resources.Region,
            Style = null
        };

        Map.Layers.Add(_hypocentreLayer, _hypocentreGroup);
        Map.Layers.Add(_stationLayer, _stationGroup);
        Map.Layers.Add(_regionLayer, _regionGroup);
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

    /// <summary>
    /// The factor to extend the view by when zooming to the earthquake.
    /// </summary>
    private const double _extendFactor = 1.2;

    /// <summary>
    /// The layer group for the hypocentre, on the top.
    /// </summary>
    private const int _hypocentreGroup = 2;
    /// <summary>
    /// The layer group for the observation stations, in the middle.
    /// </summary>
    private const int _stationGroup = 1;
    /// <summary>
    /// The layer group for the regions, on the bottom.
    /// </summary>
    private const int _regionGroup = 0;

    /// <summary>
    /// The layer for the hypocentre.
    /// </summary>
    private readonly MemoryLayer _hypocentreLayer;

    /// <summary>
    /// The layer for the observation station.
    /// </summary>
    private readonly MemoryLayer _stationLayer = new()
    {
        Style = null,
    };

    /// <summary>
    /// The layer for the regions.
    /// </summary>
    private readonly Layer _regionLayer;

    /// <summary>
    /// The cancellation token source for setting an earthquake detail.
    /// </summary>
    private CancellationTokenSource _cts = new();

    /// <summary>
    /// Selects the best telegram to display the earthquake information.
    /// </summary>
    /// <param name="telegrams">The list of earthquake telegrams available.</param>
    /// <returns>The selected eearthquake telegram.</returns>
    private TelegramItem? FindEarthquakeTelegram(IEnumerable<TelegramItem> telegrams)
        => telegrams
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

    /// <summary>
    /// Handles the selected earthquake changed.
    /// </summary>
    /// <param name="value">The new selected earthquake.</param>
    async partial void OnSelectedEarthquakeChanged(EarthquakeItemTemplate? value)
    {
        CancellationToken token = ResetDetails();

        if (value is null)
        {
            return;
        }

        GdEarthquakeEvent? rsp = await _apiCaller.GetPathEarthquakeEventAsync(value.EventId);
        IEnumerable<TelegramItem> telegrams = rsp?.EarthquakeEvent.Telegrams ?? [];
        TelegramItem? telegram = FindEarthquakeTelegram(telegrams);
        EarthquakeInformationSchema? telegramInfo = telegram is not null
            ? await _telegramRetriever.GetJsonTelegramAsync(telegram.Id) as EarthquakeInformationSchema
            : null;

        IEnumerable<DetailIntensityTemplate>? tree = null;
        MRect? regionLimits = null;
        if (telegramInfo?.Body.Intensity is not null)
        {
            await LoadEarthquakeObservationStationsAsync();
            (regionLimits, tree) = DealWithTelegramIntensities(telegramInfo.Body.Intensity, token);
        }

        // Mark Hypocentre
        MRect? hypocentreLimits = null;
        CoordinateComponent? coordinate = telegramInfo?.Body.Earthquake?.Hypocentre.Coordinates ?? value.Hypocentre?.Coordinates;
        if (coordinate?.Longitude is not null && coordinate?.Latitude is not null)
        {
            hypocentreLimits = DealWithHypocentre(coordinate.Longitude, coordinate.Latitude, token);
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
            EarthquakeDetails = new(value, telegramInfo, tree);
            Map.Navigator.ZoomToBox(limits);
        }
    }

    /// <summary>
    /// Deals with the hypocentre of the earthquake.
    /// </summary>
    /// <param name="longitude">The longitude of the hypocentre.</param>
    /// <param name="latitude">The latitude of the hypocentre.</param>
    /// <param name="token">The token with whether to cancel the operation.</param>
    /// <returns>The limits of the earthqukae in <see cref="MRect"/>.</returns>
    private MRect DealWithHypocentre(
        Coordinate longitude,
        Coordinate latitude,
        CancellationToken token)
    {
        MPoint convertedCoordinates = SphericalMercator
                        .FromLonLat(
                        longitude.DoubleValue,
                        latitude.DoubleValue)
                        .ToMPoint();
        MRect limits = convertedCoordinates.MRect;

        if(!token.IsCancellationRequested)
        {
            _hypocentreLayer.Features = [new PointFeature(convertedCoordinates)];
            _hypocentreLayer.DataHasChanged();
        }

        return limits;
    }

    /// <summary>
    /// Deals with the intensities of the earthquake in the telegram.
    /// </summary>
    /// <param name="intensityDetails">The details of the intensities in the telegram.</param>
    /// <param name="token">The token with whether to cancel the operation.</param>
    /// <returns>A tuple of: the limits of the regions, and the intensity tree.</returns>
    private (
        MRect regionLimits,
        IEnumerable<DetailIntensityTemplate> intensityTree) DealWithTelegramIntensities(
        IntensityDetails intensityDetails,
        CancellationToken token)
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

        if (!token.IsCancellationRequested)
        {
            _regionLayer.Style = intensityDetails.Regions.ToRegionStyle();
            _regionLayer.DataHasChanged();
            _stationLayer.Features = stationData.Select(x => x.ToStationFeature());
            _stationLayer.DataHasChanged();
        }

        return (
            CreateRegionLimits(intensityDetails),
            CreateIntensityTree(stationData));
    }
    /// <summary>
    /// Creates the limits of the regions from the intensity details.
    /// </summary>
    /// <param name="intensityDetails">The intensity details from a tekegram.</param>
    /// <returns>The intensity details.</returns>
    private MRect CreateRegionLimits(IntensityDetails intensityDetails)
        => _regionFeatures
            .Join(intensityDetails.Regions,
            f => (string)f["code"]!,
            r => r.Code,
            (f, r) => f.Extent)
            .NotNull()
            .Aggregate((region1, region2) => region1.Join(region2));

    /// <summary>
    /// Creates a tree of intensities from the station data.
    /// </summary>
    /// <param name="stationData">The data of the stations.</param>
    /// <returns>The list of detail intensity template, which represents the tree, in descending intensity.</returns>
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
    /// <summary>
    /// Resets the details of the earthquake.
    /// </summary>
    /// <returns>A cancellation token for setting the next earthquake.</returns>
    private CancellationToken ResetDetails()
    {
        _cts.Cancel();
        _cts.Dispose();

        _hypocentreLayer.Features = [];
        _hypocentreLayer.DataHasChanged();
        _stationLayer.Features = [];
        _stationLayer.DataHasChanged();
        _regionLayer.Style = null;
        _regionLayer.DataHasChanged();

        Map.Navigator.ZoomToBox(_mainLimitsOfJapan);
        EarthquakeDetails = null;

        _cts = new CancellationTokenSource();
        CancellationToken token = _cts.Token;
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
    private async Task LoadEarthquakeObservationStationsAsync()
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
        => await LoadEarthquakeObservationStationsAsync();
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
