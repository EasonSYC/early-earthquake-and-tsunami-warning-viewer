﻿using System.Collections.ObjectModel;
using System.Text;
using System.Timers;
using Avalonia.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using EasonEetwViewer.Dmdata.Api.Abstractions;
using EasonEetwViewer.Dmdata.Authentication.Abstractions;
using EasonEetwViewer.Dmdata.Dtos.Enum;
using EasonEetwViewer.Dmdata.Telegram.Abstractions;
using EasonEetwViewer.Dmdata.Telegram.Dtos.EewInformation;
using EasonEetwViewer.Dmdata.Telegram.Dtos.Schema;
using EasonEetwViewer.Dmdata.Telegram.Dtos.TelegramBase;
using EasonEetwViewer.Dmdata.Telegram.Dtos.TsunamiInformation;
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
using Mapsui.Rendering.Skia.Extensions;
using Mapsui.Styles;
using Mapsui.Styles.Thematics;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using Coordinate = NetTopologySuite.Geometries.Coordinate;
using IFeature = Mapsui.IFeature;
using Polygon = NetTopologySuite.Geometries.Polygon;

namespace EasonEetwViewer.ViewModels;

internal partial class RealtimePageViewModel : MapViewModelBase
{
    public RealtimePageViewModel(
        IImageFetch imageFetch,
        IPointExtract pointExtract,
        ITimeTable timeTableProvider,
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
        _timer = new System.Timers.Timer(1000)
        {
            AutoReset = true,
            Enabled = true,
        };
        _timer.Elapsed += OnTimedEvent;

        _webSocketClient = webSocketClient;
        _webSocketClient.DataReceived += WebSocketClientDataReceivedEventHandler;

        _timeTableProvider = timeTableProvider;

        _cts = new();
        _token = _cts.Token;

        Map.Layers.Add(_kmoniLayer);

        Task.Run(StartLongRunning).Wait();
    }
    /// <summary>
    /// The logger to be used.
    /// </summary>
    private readonly ILogger<RealtimePageViewModel> _logger;

    private async Task StartLongRunning()
    {
        _ = await Task.Factory.StartNew(SwitchEew, _token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        _ = await Task.Factory.StartNew(ClearExpiredEew, _token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        _ = await Task.Factory.StartNew(ClearExpiredTsunami, _token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
    }

    private readonly IWebSocketClient _webSocketClient;
    private readonly CancellationTokenSource _cts;
    private readonly CancellationToken _token;

    #region eew
    private const string _eewHypocentreLayerPrefix = "Hypocentre";
    private const string _eewRegionLayerPrefix = "Regions";

    private readonly ITimeTable _timeTableProvider;
    private readonly TimeSpan _eewLifeTime = TimeSpan.FromMinutes(3);

    [ObservableProperty]
    private ObservableCollection<EewDetailsTemplate> _liveEewList = [];
    public EewDetailsTemplate? CurrentDisplayEew
        => CurrentEewIndex is null || CurrentEewIndex >= LiveEewList.Count
            ? null
            : LiveEewList[(int)CurrentEewIndex];

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentDisplayEew))]
    private uint? _currentEewIndex = null;

    private async Task OnEewReceived(EewInformationSchema eew)
    {
        if (!int.TryParse(eew.SerialNo, out int serial))
        {
            serial = 0;
        }

        for (int i = 0; i < LiveEewList.Count; ++i)
        {
            if (LiveEewList[i].EventId == eew.EventId)
            {
                if (LiveEewList[i].Serial > serial)
                {
                    return;
                }

                RemoveEewAt(i);
                break;
            }
        }

        DateTimeOffset lifeTime = eew.PressDateTime + _eewLifeTime;
        if (lifeTime < _timeProvider.Now())
        {
            return;
        }

        EewDetailsTemplate eewTemplate = new(eew, lifeTime, serial);
        LiveEewList.Add(eewTemplate);

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
                Map.Layers.Add(layer);
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
                Map.Layers.Add(new RasterizingLayer(layer));
            }
        }

        _ = await Task.Factory.StartNew(() => DrawEewCircles(eewTemplate), eewTemplate.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
    }

    private readonly TimeSpan _switchEewInterval = TimeSpan.FromSeconds(2.5);
    private async Task SwitchEew()
    {
        while (!_token.IsCancellationRequested)
        {
            CurrentEewIndex =
                LiveEewList.Count == 0
                    ? null
                    : CurrentEewIndex is null
                        ? 0
                        : (uint)((CurrentEewIndex + 1) % LiveEewList.Count);

            await Task.Delay(_switchEewInterval);
        }
    }

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
        if (eew.Earthquake is null
            || eew.Earthquake.Hypocentre.Depth.Value is null
            || eew.Earthquake.Hypocentre.Coordinate.Latitude is null
            || eew.Earthquake.Hypocentre.Coordinate.Longitude is null
            || eew.Earthquake.OriginTime is null
            || eew.Earthquake.Condition is "仮定震源要素")
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

        MemoryLayer? pLayer = null;
        MemoryLayer? sLayer = null;

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

                if (pLayer is null)
                {
                    pLayer = new MemoryLayer
                    {
                        Name = pLayerName,
                        Features = [pCirclePolygon.ToFeature()],
                        Style = _pCircleStyle
                    };

                    Map.Layers.Add(pLayer);
                }
                else
                {
                    pLayer.Features = [pCirclePolygon.ToFeature()];
                    pLayer.DataHasChanged();
                }
            }

            if (sDistance != 0)
            {
                Polygon sCirclePolygon = CreateCircleRing(latitude, longitude, sDistance);

                if (sLayer is null)
                {
                    sLayer = new MemoryLayer
                    {
                        Name = sLayerName,
                        Features = [sCirclePolygon.ToFeature()],
                        Style = _sCircleStyle
                    };

                    Map.Layers.Add(sLayer);
                }
                else
                {
                    sLayer.Features = [sCirclePolygon.ToFeature()];
                    sLayer.DataHasChanged();
                }
            }

            await Task.Delay(_refreshCircleInterval);
        }

        _ = Map.Layers.Remove(x => x.Name == pLayerName);
        _ = Map.Layers.Remove(x => x.Name == sLayerName);
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

    private readonly TimeSpan _removeExpiredEewInterval = TimeSpan.FromSeconds(2);
    private async Task ClearExpiredEew()
    {
        while (!_token.IsCancellationRequested)
        {
            for (int i = LiveEewList.Count - 1; i >= 0; --i)
            {
                if (LiveEewList[i].ExpiryTime < _timeProvider.Now())
                {
                    RemoveEewAt(i);
                }
            }

            await Task.Delay(_removeExpiredEewInterval);
        }
    }

    private void RemoveEewAt(int i)
    {
        EewDetailsTemplate eew = LiveEewList[i];
        eew.TokenSource.Cancel();
        eew.TokenSource.Dispose();
        LiveEewList.RemoveAt(i);
        _ = Map.Layers.Remove(x => x.Name == (_eewHypocentreLayerPrefix + eew.EventId));
        _ = Map.Layers.Remove(x => x.Name == (_eewRegionLayerPrefix + eew.EventId));
    }
    #endregion

    #region tsunami
    /// <summary>
    /// The name of the tsunami warning layer.
    /// </summary>
    private const string _tsunamiWarningLayerName = "Tsunami";
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
            _ = Map.Layers.Remove(layer => layer.Name == _tsunamiWarningLayerName);
            ILayer layer = new Layer()
            {
                Name = _tsunamiWarningLayerName,
                DataSource = _resources.Tsunami,
                Style = tsunami.Body.Tsunami.Forecasts.ToRegionStyle()
            };
            Map.Layers.Add(layer.ToRasterizingLayer());
        }

        CurrentTsunami = new(tsunami, validDateTime);
    }

    /// <summary>
    /// The time interval to check for expired tsunamis.
    /// </summary>
    private readonly TimeSpan _removeExpiredTsunamiInterval = TimeSpan.FromMinutes(1);
    /// <summary>
    /// Clears the expired tsunami warnings.
    /// </summary>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    private async Task ClearExpiredTsunami()
    {
        while (!_token.IsCancellationRequested)
        {
            if (CurrentTsunami is not null && CurrentTsunami.ExpiryTime < _timeProvider.Now())
            {
                CurrentTsunami = null;
                _ = Map.Layers.Remove(l => l.Name == _tsunamiWarningLayerName);
            }

            await Task.Delay(_removeExpiredTsunamiInterval);
        }
    }
    #endregion

    #region websocket
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
    /// The timer to trigger the event to refresh the layer and the time.
    /// </summary>
    private readonly System.Timers.Timer _timer;
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
    /// The name of the kmoni layer.
    /// </summary>
    private const string _kmoniLayerName = "KmoniLayer";
    /// <summary>
    /// The kmoni layer.
    /// </summary>
    private readonly MemoryLayer _kmoniLayer = new(_kmoniLayerName) { Style = null };

    // Adapted from https://mapsui.com/samples/ - Info - Custom Callout
    /// <summary>
    /// Handles the timed event, by changing the displaying time, and refreshing the Kmoni layer.
    /// </summary>
    /// <param name="source">The source of the event.</param>
    /// <param name="e">The event arguments.</param>
    private async void OnTimedEvent(object? source, ElapsedEventArgs e)
    {
        OnPropertyChanged(nameof(TimeDisplay));

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
        catch (ArgumentException ae)
        {
            _logger.LogWarning($"ArgumentException: {ae.Message}, {ae.Source}");
        }

        return null;
    }
    #endregion
}
