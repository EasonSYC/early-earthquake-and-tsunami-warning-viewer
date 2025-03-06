using System.Collections.ObjectModel;
using System.Timers;
using CommunityToolkit.Mvvm.ComponentModel;
using EasonEetwViewer.Dmdata.Api.Abstractions;
using EasonEetwViewer.Dmdata.Authentication.Abstractions;
using EasonEetwViewer.Dmdata.Telegram.Abstractions;
using EasonEetwViewer.Dmdata.Telegram.Dtos.EewInformation;
using EasonEetwViewer.Dmdata.Telegram.Dtos.Schema;
using EasonEetwViewer.Dmdata.Telegram.Dtos.TelegramBase;
using EasonEetwViewer.Dmdata.WebSocket.Abstractions;
using EasonEetwViewer.Dmdata.WebSocket.Events;
using EasonEetwViewer.Extensions;
using EasonEetwViewer.JmaTravelTime.Abstractions;
using EasonEetwViewer.KyoshinMonitor.Abstractions;
using EasonEetwViewer.KyoshinMonitor.Extensions;
using EasonEetwViewer.Models.RealTimePage;
using EasonEetwViewer.Services;
using EasonEetwViewer.Services.Kmoni.Abstractions;
using EasonEetwViewer.Services.TimeProvider;
using EasonEetwViewer.ViewModels.ViewModelBases;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Nts.Extensions;
using Mapsui.Projections;
using Mapsui.Styles;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using Coordinate = NetTopologySuite.Geometries.Coordinate;
using IFeature = Mapsui.IFeature;
using Polygon = NetTopologySuite.Geometries.Polygon;
using Timer = System.Timers.Timer;

namespace EasonEetwViewer.ViewModels;

/// <summary>
/// The view model for the real-time page.
/// </summary>
internal partial class RealtimePageViewModel : MapViewModelBase
{
    /// <summary>
    /// Creates a new instance of the <see cref="RealtimePageViewModel"/> class.
    /// </summary>
    /// <param name="imageFetch">The image fetcher to be used.</param>
    /// <param name="pointExtract">The point extractor to be used.</param>
    /// <param name="timeTable">The time table to be used.</param>
    /// <param name="kmoniOptions">The kmoni options to be used.</param>
    /// <param name="webSocketClient">The WebSocket client to be used.</param>
    /// <param name="resources">The map resources to be used.</param>
    /// <param name="authenticatorWrapper">The authenticator to be used.</param>
    /// <param name="apiCaller">The API caller to be used.</param>
    /// <param name="telegramRetriever">The telegram retriever to be used.</param>
    /// <param name="telegramParser">The telegram parser to be used.</param>
    /// <param name="timeProvider">The time provider to be used.</param>
    /// <param name="logger">The logger to be used.</param>
    public RealtimePageViewModel(
        IImageFetch imageFetch,
        IPointExtract pointExtract,
        ITimeTable timeTable,
        IKmoniSettingsHelper kmoniOptions,
        IWebSocketClient webSocketClient,
        MapResourcesProvider resources,
        IAuthenticationHelper authenticatorWrapper,
        IApiCaller apiCaller,
        ITelegramRetriever telegramRetriever,
        ITelegramParser telegramParser,
        ITimeProvider timeProvider,
        ILogger<RealtimePageViewModel> logger)
    : base(resources,
        authenticatorWrapper,
        apiCaller,
        telegramRetriever,
        telegramParser,
        timeProvider,
        logger)
    {
        _logger = logger;
        KmoniOptions = kmoniOptions;
        _imageFetch = imageFetch;
        _pointExtract = pointExtract;

        _timer1000 = new Timer(1000)
        {
            AutoReset = true,
            Enabled = true
        };
        _timer1000.Elapsed += RefreshKmoniDataEventHandler;
        _timer1000.Elapsed += RemoveExpiredTsunamiEventHandler;
        _timer1000.Elapsed += RemoveExpiredEewEventHandler;
        _timer1000.Elapsed += (o, e)
            => OnPropertyChanged(nameof(TimeDisplay));

        _timer2500 = new Timer(2500)
        {
            AutoReset = true,
            Enabled = true
        };
        _timer2500.Elapsed += SwitchEew;

        _webSocketClient = webSocketClient;
        _webSocketClient.DataReceived += WebSocketClientDataReceivedEventHandler;

        _timeTableProvider = timeTable;

        _tsunamiLayer = new()
        {
            DataSource = _resources.Tsunami,
            Style = null
        };

        Map.Layers.Add(_kmoniLayer, _kmoniGroup);
        Map.Layers.Add(_tsunamiLayer, _tsunamiGroup);
    }
    /// <summary>
    /// The logger to be used.
    /// </summary>
    private readonly ILogger<RealtimePageViewModel> _logger;
    /// <summary>
    /// The timer to trigger events every second.
    /// </summary>
    /// <remarks>
    /// Event trigerred includes: Refreshing Time Display, Remove Expired Warnings, Refresh kmoni Layer.
    /// </remarks>
    private readonly Timer _timer1000;
    /// <summary>
    /// The timer to trigger events every 2.5 seconds.
    /// </summary>
    /// <remarks>
    ///  Event trigerred includes: Switching EEW.
    ///  </remarks>
    private readonly Timer _timer2500;

    /// <summary>
    /// The group for the hypocentre layer, on the top.
    /// </summary>
    private const int _hypocentreGroup = 4;
    /// <summary>
    /// The group for the wavefront layer, below the hypocentre layer.
    /// </summary>
    private const int _wavefrontGroup = 3;
    /// <summary>
    /// The group for the tsunami warning layer, below the wavefront layer.
    /// </summary>
    private const int _tsunamiGroup = 2;
    /// <summary>
    /// The group for the kmoni layer, below the tsunami warning layer.
    /// </summary>
    private const int _kmoniGroup = 1;
    /// <summary>
    /// The group for the intensity layer, at the bottom.
    /// </summary>
    private const int _intensityGroup = 0;

    #region eew
    private const string _eewHypocentreLayerPrefix = "Hypocentre";
    private const string _eewRegionLayerPrefix = "Regions";

    private readonly ITimeTable _timeTableProvider;
    private readonly TimeSpan _eewLifeTime = TimeSpan.FromMinutes(3);

    private readonly Dictionary<string, EewDetailsTemplate> _liveEewList = [];
    public EewDetailsTemplate? CurrentDisplayEew
        => CurrentEewIndex is null || CurrentEewIndex >= _liveEewList.Count
            ? null
            : _liveEewList.ToList()[(int)CurrentEewIndex].Value;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentDisplayEew))]
    private int? _currentEewIndex = null;

    private async Task OnEewReceived(EewInformationSchema eew)
    {
        if (!int.TryParse(eew.SerialNo, out int serial) || eew.EventId is null)
        {
            return;
        }

        if (_liveEewList.TryGetValue(eew.EventId, out EewDetailsTemplate? existingEew))
        {
            if (existingEew.Serial > serial)
            {
                return;
            }

            RemoveEew(existingEew);
        }

        DateTimeOffset lifeTime = eew.PressDateTime + _eewLifeTime;
        if (lifeTime < _timeProvider.Now())
        {
            return;
        }

        EewDetailsTemplate eewTemplate = new(eew, lifeTime, serial);
        _liveEewList[eew.EventId] = eewTemplate;

        if (eew.Body.IsCancelled)
        {
            return;
        }

        if (eew.Body.Earthquake?.Hypocentre.Coordinate.Longitude is not null &&
            eew.Body.Earthquake?.Hypocentre.Coordinate.Latitude is not null)
        {
            MPoint point = SphericalMercator.FromLonLat(
                eew.Body.Earthquake.Hypocentre.Coordinate.Longitude!.DoubleValue,
                eew.Body.Earthquake.Hypocentre.Coordinate.Latitude!.DoubleValue).ToMPoint();

            MemoryLayer layer = new()
            {
                Name = _eewHypocentreLayerPrefix + eew.EventId,
                Features = [new PointFeature(point)],
                Style = eew.Body.Earthquake.Condition is "仮定震源要素" ? _resources.PlumShapeStyle : _resources.HypocentreShapeStyle
            };

            if (!eewTemplate.Token.IsCancellationRequested)
            {
                Map.Layers.Add(layer, _hypocentreGroup);
            }
        }

        if (eew.Body.Intensity is not null)
        {
            IEnumerable<Region> regions = eew.Body.Intensity.Regions;

            ILayer layer = new Layer()
            {
                Name = _eewRegionLayerPrefix + eew.EventId,
                DataSource = _resources.Region,
                Style = regions.ToRegionStyle()
            };

            if (!eewTemplate.Token.IsCancellationRequested)
            {
                Map.Layers.Add(new RasterizingLayer(layer), _intensityGroup);
            }
        }

        _ = await Task.Factory.StartNew(() => DrawEewCircles(eewTemplate), eewTemplate.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
    }
    private void SwitchEew(object? sender, ElapsedEventArgs e)
        => CurrentEewIndex =
            _liveEewList.Count == 0
                ? null
                : CurrentEewIndex is null
                    ? 0
                    : (CurrentEewIndex + 1) % _liveEewList.Count;

    private readonly IStyle _pCircleStyle
        = new VectorStyle
        {
            Outline = new Pen
            {
                Color = Color.Blue,
                Width = 2
            },
            Fill = null
        };
    private readonly IStyle _sCircleStyle
        = new VectorStyle
        {
            Outline = new Pen
            {
                Color = Color.Orange,
                Width = 2
            },
            Fill = null
        };
    private const string _eewWavefrontLayerPrefix = "Wavefront";
    private readonly TimeSpan _refreshCircleInterval = TimeSpan.FromSeconds(1d / 60d);
    private async Task DrawEewCircles(EewDetailsTemplate eew)
    {
        if (eew.Earthquake is null ||
            eew.Earthquake.Hypocentre.Depth.Value is null ||
            eew.Earthquake.Hypocentre.Coordinate.Latitude is null ||
            eew.Earthquake.Hypocentre.Coordinate.Longitude is null ||
            eew.Earthquake.OriginTime is null ||
            eew.Earthquake.Condition is "仮定震源要素")
        {
            return;
        }

        int depth = (int)eew.Earthquake.Hypocentre.Depth.Value;
        if (depth is < 0 or > 700)
        {
            return;
        }

        string pLayerName = _eewWavefrontLayerPrefix + "P" + eew.EventId;
        string sLayerName = _eewWavefrontLayerPrefix + "S" + eew.EventId;

        double latitude = eew.Earthquake.Hypocentre.Coordinate.Latitude.DoubleValue;
        double longitude = eew.Earthquake.Hypocentre.Coordinate.Longitude.DoubleValue;

        MemoryLayer pLayer = new()
        {
            Name = pLayerName,
            Features = [],
            Style = _pCircleStyle
        };
        MemoryLayer sLayer = new()
        {
            Name = sLayerName,
            Features = [],
            Style = _sCircleStyle
        };

        Map.Layers.Add(pLayer, _wavefrontGroup);
        Map.Layers.Add(sLayer, _wavefrontGroup);

        while (!eew.Token.IsCancellationRequested)
        {
            double time = ((TimeSpan)(_timeProvider.Now() - eew.Earthquake.OriginTime)).TotalSeconds;

            if (time < 0)
            {
                continue;
            }

            (double pDistance, double sDistance) = _timeTableProvider.DistanceFromDepthTime(depth, time);

            if (pDistance != 0)
            {
                Polygon pCirclePolygon = CreateCircleRing(latitude, longitude, pDistance);
                pLayer.Features = [pCirclePolygon.ToFeature()];
                pLayer.DataHasChanged();
            }

            if (sDistance != 0)
            {
                Polygon sCirclePolygon = CreateCircleRing(latitude, longitude, sDistance);
                sLayer.Features = [sCirclePolygon.ToFeature()];
                sLayer.DataHasChanged();
            }

            await Task.Delay(_refreshCircleInterval);
        }

        _ = Map.Layers.Remove(pLayer);
        _ = Map.Layers.Remove(sLayer);
    }
    private static Polygon CreateCircleRing(double latitude, double longitude, double radius, double quality = 360)
    {
        (double x, double y) = SphericalMercator.FromLonLat(longitude, latitude);
        radius *= 1000;
        radius *= 1.23; // a magic constant found by experimenting

        ICollection<Coordinate> outerRing = [];
        double increment = 360d / (quality < 3 ? 3 : (quality > 360 ? 360 : quality));
        for (double angle = 0; angle < 360; angle += increment)
        {
            double angleInRadians = angle / 180 * Math.PI;
            outerRing.Add(new Coordinate(x + (Math.Sin(angleInRadians) * radius), y + (Math.Cos(angleInRadians) * radius)));
        }

        outerRing.Add(outerRing.ElementAt(0));

        return new Polygon(new LinearRing([.. outerRing]));
    }
    private void RemoveExpiredEewEventHandler(object? sender, ElapsedEventArgs e)
    {
        foreach (string eventId in _liveEewList.Keys.ToList())
        {
            if (!_liveEewList.TryGetValue(eventId, out EewDetailsTemplate? eew))
            {
                continue;
            }

            if (eew.ExpiryTime < _timeProvider.Now())
            {
                RemoveEew(eew);
                _ = _liveEewList.Remove(eventId);
            }
        }
    }

    private void RemoveEew(EewDetailsTemplate eew)
    {
        eew.TokenSource.Cancel();
        eew.TokenSource.Dispose();
        _ = Map.Layers.Remove(x => x.Name == (_eewHypocentreLayerPrefix + eew.EventId));
        _ = Map.Layers.Remove(x => x.Name == (_eewRegionLayerPrefix + eew.EventId));
    }
    #endregion

    #region tsunami
    /// <summary>
    /// The current tsunami.
    /// </summary>
    [ObservableProperty]
    private TsunamiDetailsTemplate? _currentTsunami = null;
    /// <summary>
    /// The default lifetime for a tsunami.
    /// </summary>
    private readonly TimeSpan _tsunamiLifeTime = TimeSpan.FromDays(2);
    /// <summary>
    /// The map layer for tsunami warnings.
    /// </summary>
    private readonly Layer _tsunamiLayer;
    /// <summary>
    /// Handles when a new tsunami telegram is received.
    /// </summary>
    /// <param name="tsunami">The tsunami telegram.</param>
    private void OnTsunamiReceived(TsunamiInformationSchema tsunami)
    {
        DateTimeOffset validDateTime = tsunami.ValidDateTime ?? tsunami.PressDateTime + _tsunamiLifeTime;
        if (validDateTime < _timeProvider.Now())
        {
            return;
        }

        if (tsunami.Body.Tsunami?.Forecasts is not null)
        {
            _tsunamiLayer.Style = tsunami.Body.Tsunami.Forecasts.ToRegionStyle();
            _tsunamiLayer.DataHasChanged();
        }

        CurrentTsunami = new(tsunami, validDateTime);
    }
    /// <summary>
    /// Clears the expired tsunami warnings.
    /// </summary>
    /// <param name="source">The source of the event.</param>
    /// <param name="e">The event arguments.</param>
    private void RemoveExpiredTsunamiEventHandler(object? source, ElapsedEventArgs e)
    {
        if (CurrentTsunami is not null && CurrentTsunami.ExpiryTime < _timeProvider.Now())
        {
            CurrentTsunami = null;
            _tsunamiLayer.Style = null;
            _tsunamiLayer.DataHasChanged();
        }
    }
    #endregion

    #region websocket
    /// <summary>
    /// The WebSocket client to be used.
    /// </summary>
    private readonly IWebSocketClient _webSocketClient;
    /// <summary>
    /// Raises when there is data received over the WebSocket connection.
    /// </summary>
    public event EventHandler? WebSocketDataReceived;
    /// <summary>
    /// Handles when data is received by the WebSocket connection.
    /// </summary>
    /// <param name="sender">The sender of hte event.</param>
    /// <param name="e">The event arguments.</param>
    internal async void WebSocketClientDataReceivedEventHandler(object? sender, DataReceivedEventArgs e)
    {
        WebSocketDataReceived?.Invoke(this, new());

        Head telegram = e.Telegram;
        if (telegram is EewInformationSchema eew)
        {
            await OnEewReceived(eew);
        }
        else if (telegram is TsunamiInformationSchema tsunami)
        {
            OnTsunamiReceived(tsunami);
        }
    }
    #endregion

    #region kmoni
    /// <summary>
    /// The options for the kmoni layer.
    /// </summary>
    private IKmoniSettingsHelper KmoniOptions { get; init; }
    /// <summary>
    /// The time display.
    /// </summary>
    public DateTimeOffset TimeDisplay
        => _timeProvider.Now();
    /// <summary>
    /// The image fetcher to be used.
    /// </summary>
    private readonly IImageFetch _imageFetch;
    /// <summary>
    /// The point extractor to be used.
    /// </summary>
    private readonly IPointExtract _pointExtract;
    /// <summary>
    /// The delay for the kmoni layer when fetching data.
    /// </summary>
    private readonly TimeSpan _kmoniDelay = new(0, 0, 1);
    /// <summary>
    /// The kmoni layer.
    /// </summary>
    private readonly MemoryLayer _kmoniLayer = new() { Style = null };

    // Adapted from https://mapsui.com/samples/ - Info - Custom Callout
    /// <summary>
    /// Handles the timed event, by changing the displaying time, and refreshing the Kmoni layer.
    /// </summary>
    /// <param name="source">The source of the event.</param>
    /// <param name="e">The event arguments.</param>
    private async void RefreshKmoniDataEventHandler(object? source, ElapsedEventArgs e)
    {
        IEnumerable<IFeature>? kmoniObservationPoints = await GetKmoniObservationPoints();
        if (kmoniObservationPoints is null)
        {
            return;
        }

        _kmoniLayer.Features = kmoniObservationPoints;
        _kmoniLayer.DataHasChanged();
    }

    /// <summary>
    /// Gets the observation points from Kmoni, and convert them to features.
    /// </summary>
    /// <returns>The list of features, or <see langword="null"/> if it was unsuccessful.</returns>
    private async Task<IEnumerable<IFeature>?> GetKmoniObservationPoints()
    {
        try
        {
            byte[]? imageBytes = await _imageFetch
                .GetByteArrayAsync(
                KmoniOptions.MeasurementChoice,
                KmoniOptions.SensorChoice,
                _timeProvider
                    .ConvertToJst(_timeProvider.Now())
                    .Subtract(_kmoniDelay)
                    .DateTime);

            return imageBytes is null
                ? null
                : _pointExtract
                    .ExtractColours(imageBytes.ToBitmap(), KmoniOptions.SensorChoice is SensorType.Borehole)
                    .Select(pc => pc.ToStationFeature());
        }
        catch (ArgumentException) { }

        return null;
    }
    #endregion
}
