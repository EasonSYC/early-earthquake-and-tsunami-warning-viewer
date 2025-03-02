using System.Collections.ObjectModel;
using System.Text;
using System.Timers;
using Avalonia.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using EasonEetwViewer.Api.Abstractions;
using EasonEetwViewer.Authentication.Abstractions;
using EasonEetwViewer.Dtos.Enum;
using EasonEetwViewer.Extensions;
using EasonEetwViewer.JmaTravelTime.Abstractions;
using EasonEetwViewer.KyoshinMonitor.Abstractions;
using EasonEetwViewer.KyoshinMonitor.Extensions;
using EasonEetwViewer.Models.RealTimePage;
using EasonEetwViewer.Services;
using EasonEetwViewer.Services.KmoniOptions;
using EasonEetwViewer.Services.TimeProvider;
using EasonEetwViewer.Telegram.Abstractions;
using EasonEetwViewer.Telegram.Dtos.EewInformation;
using EasonEetwViewer.Telegram.Dtos.Schema;
using EasonEetwViewer.Telegram.Dtos.TelegramBase;
using EasonEetwViewer.Telegram.Dtos.TsunamiInformation;
using EasonEetwViewer.ViewModels.ViewModelBases;
using EasonEetwViewer.WebSocket.Abstractions;
using EasonEetwViewer.WebSocket.Events;
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
        KmoniOptions kmoniOptions,
        IWebSocketClient webSocketClient,
        MapResourcesProvider resources,
        IAuthenticationHelper authenticatorWrapper,
        IApiCaller apiCaller,
        ITelegramRetriever telegramRetriever,
        ITimeProvider timeProvider,
        ILogger<RealtimePageViewModel> logger)
    : base(resources,
        authenticatorWrapper,
        apiCaller,
        telegramRetriever,
        timeProvider,
        logger)
    {
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
        _webSocketClient.DataReceived += WebSocketClient_DataReceived;

        _timeTableProvider = timeTableProvider;

        _cts = new();
        _token = _cts.Token;

        Task.Run(StartLongRunning).Wait();
    }

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

    private static string ToInformationString(EewInformationSchema eew)
    {
        StringBuilder sb = new();
        if (eew.Headline is not null)
        {
            _ = sb.AppendLine(eew.Headline);
        }

        if (eew.Body.Text is not null)
        {
            _ = sb.AppendLine(eew.Body.Text);
        }

        if (eew.Body.Comments is not null)
        {
            if (eew.Body.Comments.FreeText is not null)
            {
                _ = sb.AppendLine(eew.Body.Comments.FreeText);
            }

            if (eew.Body.Comments.Warning is not null)
            {
                _ = sb.AppendLine(eew.Body.Comments.Warning.Text);
            }
        }

        return sb.ToString();
    }
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

        string informationalText = ToInformationString(eew);

        EewDetailsTemplate eewTemplate = new(lifeTime, eew.PressDateTime, serial, eew.EventId, eew.Body.IsCancelled, eew.Body.IsLastInfo, eew.Body.IsWarning ?? false, eew.Body.Earthquake, eew.Body.Intensity, informationalText);
        LiveEewList.Add(eewTemplate);

        if (eew.Body.IsCancelled)
        {
            return;
        }

        if (eew.Body.Earthquake is not null
            && eew.Body.Earthquake.Hypocentre.Coordinate.Longitude is not null
            && eew.Body.Earthquake.Hypocentre.Coordinate.Latitude is not null)
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
                Style = CreateRegionThemeStyle(regions)
            };

            if (!eewTemplate.Token.IsCancellationRequested)
            {
                Map.Layers.Add(new RasterizingLayer(layer));
            }
        }

        _ = await Task.Factory.StartNew(() => DrawEewCircles(eewTemplate), eewTemplate.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
    }
    private static ThemeStyle CreateRegionThemeStyle(IEnumerable<Region> regions)
        => new(f =>
            {
                Region? region = regions.FirstOrDefault(r => r.Code == f["code"]?.ToString()?.ToLower());
                return region is null
                    ? null
                    : new VectorStyle()
                    {
                        Fill = new Brush(Color.Opacity(Color.FromString(((Intensity)region.ForecastMaxInt.From).ToColourString()), 0.60f))
                    };
            });

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

    private readonly IStyle pCircleStyle
        = new VectorStyle
        {
            Outline = new Pen
            {
                Color = Color.Blue,
                Width = 2
            },
            Fill = null
        };
    private readonly IStyle sCircleStyle
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
                        Style = pCircleStyle
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
                        Style = sCircleStyle
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
        radius *= 1.23;

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
    private const string _tsunamiWarningLayerName = "Tsunami";
    [ObservableProperty]
    private TsunamiDetailsTemplate? _currentTsunami = null;
    private static string ToInformationString(TsunamiInformationSchema tsunami)
    {
        StringBuilder sb = new();
        if (tsunami.Headline is not null)
        {
            _ = sb.AppendLine(tsunami.Headline);
        }

        if (tsunami.Body.Text is not null)
        {
            _ = sb.AppendLine(tsunami.Body.Text);
        }

        if (tsunami.Body.Comments is not null)
        {
            if (tsunami.Body.Comments.FreeText is not null)
            {
                _ = sb.AppendLine(tsunami.Body.Comments.FreeText);
            }

            if (tsunami.Body.Comments.Warning is not null)
            {
                _ = sb.AppendLine(tsunami.Body.Comments.Warning.Text);
            }
        }

        return sb.ToString();
    }
    private readonly TimeSpan _tsunamiLifeTime = TimeSpan.FromDays(2);
    private void OnTsunamiReceived(TsunamiInformationSchema schema)
    {
        DateTimeOffset validDateTime = schema.ValidDateTime ?? schema.PressDateTime + _tsunamiLifeTime;
        if (validDateTime < _timeProvider.Now())
        {
            return;
        }

        TsunamiWarningType maxType = TsunamiWarningType.None;

        if (schema.Body.Tsunami is not null && schema.Body.Tsunami.Forecasts is not null)
        {
            maxType = schema.Body.Tsunami.Forecasts.Max(f => f.Kind.Code.ToTsunamiWarningType());

            _ = Map.Layers.Remove(l => l.Name == _tsunamiWarningLayerName);
            ILayer layer = new Layer()
            {
                Name = _tsunamiWarningLayerName,
                DataSource = _resources.Tsunami,
                Style = CreateForecastThemeStyle(schema.Body.Tsunami.Forecasts)
            };
            Map.Layers.Add(new RasterizingLayer(layer));
        }

        CurrentTsunami = new(ToInformationString(schema), validDateTime, schema.PressDateTime, maxType);
    }
    private static ThemeStyle CreateForecastThemeStyle(IEnumerable<Forecast> forecasts)
        => new(f =>
        {
            Forecast? forecast = forecasts.FirstOrDefault(fr => fr.Code == f["code"]?.ToString()?.ToLower());
            return forecast is null
                ? null
                : new VectorStyle()
                {
                    Line = new Pen(Color.Opacity(Color.FromString(forecast.Kind.Code.ToTsunamiWarningType().ToColourString()), 0.80f), 2.5)
                };
        });
    private readonly TimeSpan _removeExpiredTsunamiInterval = TimeSpan.FromMinutes(1);
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
    public event EventHandler? WebSocketDataReceived;
    public async void WebSocketClient_DataReceived(object? sender, DataEventArgs e)
    {
        WebSocketDataReceived?.Invoke(this, new());

        Head telegram = e.Telegram;
        if (telegram is EewInformationSchema eew)
        {
            await OnEewReceived(eew);
        }

        if (telegram is TsunamiInformationSchema tsunami)
        {
            OnTsunamiReceived(tsunami);
        }
    }
    #endregion

    #region kmoni
    internal KmoniOptions KmoniOptions { get; init; }
    internal DateTimeOffset TimeDisplay
        => _timeProvider.Now();
    private readonly System.Timers.Timer _timer;

    private const string _realTimeLayerName = "KmoniLayer";
    private const int _kmoniDelaySeconds = 1;
    private readonly IImageFetch _imageFetch;
    private readonly IPointExtract _pointExtract;
    private MemoryLayer? _kmoniLayer;

    // Adapted from https://mapsui.com/samples/ - Info - Custom Callout
    private void OnTimedEvent(object? source, ElapsedEventArgs e)
    {
        OnPropertyChanged(nameof(TimeDisplay));

        IEnumerable<IFeature>? kmoniObservationPoints = GetKmoniObservationPoints().Result;
        if (kmoniObservationPoints is null)
        {
            return;
        }

        if (_kmoniLayer is null)
        {
            _kmoniLayer = new()
            {
                Name = _realTimeLayerName,
                Features = kmoniObservationPoints,
                Style = null
            };
            Map.Layers.Add(_kmoniLayer);
        }
        else
        {
            _kmoniLayer.Features = kmoniObservationPoints;
            _kmoniLayer.DataHasChanged();
        }
    }

    private async Task<IEnumerable<IFeature>?> GetKmoniObservationPoints()
    {
        try
        {
            byte[]? imageBytes = await _imageFetch.GetByteArrayAsync(
                KmoniOptions.DataChoice,
                KmoniOptions.SensorChoice,
                _timeProvider.ConvertToJst(_timeProvider.Now())
                .AddSeconds(-_kmoniDelaySeconds)
                .DateTime);

            return imageBytes is null
                ? null
                : _pointExtract
                .ExtractColours(imageBytes.ToBitmap(), KmoniOptions.SensorChoice is SensorType.Borehole)
                .Select(pc => (pc.point, height: pc.colour.ColourToHeight()))
                .Select(pc
                    => new PointFeature(SphericalMercator.FromLonLat(pc.point.Location.Longitude, pc.point.Location.Latitude).ToMPoint())
                    {
                        Styles = [
                            new SymbolStyle()
                            {
                                SymbolScale = 0.1,
                                Fill = new Brush(pc.height.HeightToColour().ToMapsui())
                            }]
                    });
        }
        catch (ArgumentException ae)
        {
            Logger.TryGet(LogEventLevel.Warning, LogArea.Control)?.Log(this, $"ArgumentException: {ae.Message}, {ae.Source}");
        }

        return null;
    }
    #endregion
}
