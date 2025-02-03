using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Timers;
using Avalonia.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using DynamicData;
using EasonEetwViewer.Authentication;
using EasonEetwViewer.Dmdata.Caller;
using EasonEetwViewer.Dmdata.Caller.Interfaces;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Enum;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Enum.WebSocket;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.EewInformation;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.Schema;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.TelegramBase;
using EasonEetwViewer.JmaTravelTime;
using EasonEetwViewer.KyoshinMonitor;
using EasonEetwViewer.KyoshinMonitor.Dto;
using EasonEetwViewer.Lang;
using EasonEetwViewer.Models;
using EasonEetwViewer.Services;
using EasonEetwViewer.Services.KmoniOptions;
using EasonEetwViewer.ViewModels.ViewModelBases;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Nts;
using Mapsui.Nts.Extensions;
using Mapsui.Projections;
using Mapsui.Providers;
using Mapsui.Rendering.Skia.Extensions;
using Mapsui.Styles;
using Mapsui.Styles.Thematics;
using NetTopologySuite.Geometries;
using NetTopologySuite.Noding;
using SkiaSharp;
using Coordinate = NetTopologySuite.Geometries.Coordinate;
using IFeature = Mapsui.IFeature;
using Polygon = NetTopologySuite.Geometries.Polygon;

namespace EasonEetwViewer.ViewModels;

internal partial class RealtimePageViewModel : MapViewModelBase
{
    private readonly JsonSerializerOptions _options = new()
    {
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
    };
    public RealtimePageViewModel(IImageFetch imageFetch, IPointExtract pointExtract, ITimeTableProvider timeTableProvider, KmoniOptions kmoniOptions, IWebSocketClient webSocketClient,
        StaticResources resources, AuthenticatorDto authenticatorDto, IApiCaller apiCaller, ITelegramRetriever telegramRetriever, ITimeProvider timeProvider, OnAuthenticatorChanged onChange)
    : base(resources, authenticatorDto, apiCaller, telegramRetriever, timeProvider, onChange)
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
        _webSocketClient.DataReceivedAction += OnDataReceived;

        _timeTableProvider = timeTableProvider;

        _cts = new();
        _token = _cts.Token;

        Task.Run(StartLongRunning).Wait();
    }

    private async Task StartLongRunning()
    {
        _ = await Task.Factory.StartNew(SwitchEew, _token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        _ = await Task.Factory.StartNew(ClearExpiredEew, _token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
    }

    private readonly IWebSocketClient _webSocketClient;
    private readonly CancellationTokenSource _cts;
    private readonly CancellationToken _token;

    #region eew
    private const string _eewHypocentreLayerPrefix = "Hypocentre";
    private const string _eewRegionLayerPrefix = "Regions";
    private const string _eewWavefrontLayerPrefix = "Wavefront";

    private readonly ITimeTableProvider _timeTableProvider;

    private readonly TimeSpan _refreshCircleInterval = TimeSpan.FromMilliseconds(250);
    private readonly TimeSpan _switchEewInterval = TimeSpan.FromSeconds(2);
    private readonly TimeSpan _removeExpiredEewInterval = TimeSpan.FromSeconds(2);
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
        if (lifeTime < _timeProvider.DateTimeOffsetNow())
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
            };

            if (eew.Body.Earthquake.Condition is not "仮定震源要素")
            {
                // TODO: PLUM Hypocentre
                layer.Style = _resources.HypocentreShapeStyle;
            }
            else
            {
                layer.Style = _resources.HypocentreShapeStyle;
            }

            if (!eewTemplate.Token.IsCancellationRequested)
            {
                Map.Layers.Add(layer);
            }
        }

        if (eew.Body.Intensity is not null)
        {
            List<Region> regions = eew.Body.Intensity.Regions;

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
    private static ThemeStyle CreateRegionThemeStyle(List<Region> regions)
        => new(f =>
            {
                foreach (Region region in regions)
                {
                    if (f["code"]?.ToString()?.ToLower() == region.Code)
                    {
                        return new VectorStyle()
                        {
                            Fill = new Brush(Color.Opacity(Color.FromString(((Intensity)region.ForecastMaxInt.From).ToColourString()), 0.60f))
                        };
                    }
                }

                return null;
            });
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
    private async Task DrawEewCircles(EewDetailsTemplate eew)
    {
        if (eew.Earthquake is null
            || eew.Earthquake.Hypocentre.Depth.Value is null
            || eew.Earthquake.Hypocentre.Coordinate.Latitude is null
            || eew.Earthquake.Hypocentre.Coordinate.Longitude is null
            || eew.Earthquake.OriginTime is null)
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


        while (!eew.Token.IsCancellationRequested)
        {
            double time = ((TimeSpan)(_timeProvider.DateTimeOffsetNow() - eew.Earthquake.OriginTime)).TotalSeconds;
            (double pDistance, double sDistance) = _timeTableProvider.DistanceFromDepthTime(depth, time);

            if (pDistance != 0)
            {
                Polygon pCirclePolygon = CreateCircleRing(latitude, longitude, pDistance);

                ILayer pLayer = new Layer
                {
                    Name = pLayerName,
                    DataSource = new MemoryProvider(pCirclePolygon.ToFeature()),
                    Style = pCircleStyle
                };

                _ = Map.Layers.Remove(x => x.Name == pLayerName);
                Map.Layers.Add(pLayer);
            }

            if (sDistance != 0)
            {
                Polygon sCirclePolygon = CreateCircleRing(latitude, longitude, sDistance);

                ILayer sLayer = new Layer
                {
                    Name = sLayerName,
                    DataSource = new MemoryProvider(sCirclePolygon.ToFeature()),
                    Style = sCircleStyle
                };


                _ = Map.Layers.Remove(x => x.Name == sLayerName);
                Map.Layers.Add(sLayer);
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

        List<Coordinate> outerRing = [];
        double increment = 360d / (quality < 3 ? 3 : (quality > 360 ? 360 : quality));
        for (double angle = 0; angle < 360; angle += increment)
        {
            double angleInRadians = angle / 180 * Math.PI;
            outerRing.Add(new Coordinate(x + (Math.Sin(angleInRadians) * radius), y + (Math.Cos(angleInRadians) * radius)));
        }

        outerRing.Add(outerRing[0]);

        return new Polygon(new LinearRing([.. outerRing]));
    }
    private async Task ClearExpiredEew()
    {
        while (!_token.IsCancellationRequested)
        {
            for (int i = LiveEewList.Count - 1; i >= 0; --i)
            {
                if (LiveEewList[i].ExpiryTime < _timeProvider.DateTimeOffsetNow())
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
    private void OnTsunamiReceived(TsunamiInformationSchema schema)
    {
        ;
    }
    #endregion

    #region websocket
    public void OnDataReceived(string data, FormatType? format)
    {
        if (format == FormatType.Json)
        {
            Head headData = JsonSerializer.Deserialize<Head>(data) ?? throw new ArgumentNullException(nameof(data));
            if (headData.Schema.Type == "eew-information" && headData.Schema.Version == "1.0.0")
            {
                EewInformationSchema eew = JsonSerializer.Deserialize<EewInformationSchema>(data, _options)!;
                OnEewReceived(eew);
                return;
            }

            if (headData.Schema.Type == "tsunami-information" && headData.Schema.Version == "1.1.0")
            {
                TsunamiInformationSchema tsunami = JsonSerializer.Deserialize<TsunamiInformationSchema>(data, _options)!;
                OnTsunamiReceived(tsunami);
                return;
            }
        }
    }
    #endregion

    #region kmoni
    internal KmoniOptions KmoniOptions { get; init; }

    private const int _jstAheadUtcHours = 9;
    internal string TimeDisplayText => _timeProvider.DateTimeOffsetNow().ToOffset(new(_jstAheadUtcHours, 0, 0)).ToString("yyyy/MM/dd HH:mm:ss");
    private readonly System.Timers.Timer _timer;

    private const string _realTimeLayerName = "KmoniLayer";
    private const int _kmoniDelaySeconds = 1;
    private readonly IImageFetch _imageFetch;
    private readonly IPointExtract _pointExtract;

    private void OnTimedEvent(object? source, ElapsedEventArgs e)
    {
        OnPropertyChanged(nameof(TimeDisplayText));
        ILayer? newLayer = GetKmoniLayer();
        if (newLayer is not null)
        {
            _ = Map.Layers.Remove(x => x.Name == _realTimeLayerName);
            Map.Layers.Add(newLayer);
        }
    }

    // Adapted from https://mapsui.com/samples/ - Info - Custom Callout

    private MemoryLayer? GetKmoniLayer()
    {
        IEnumerable<IFeature>? kmoniObservationPoints = GetKmoniObservationPoints();
        return kmoniObservationPoints is null
            ? null
            : new MemoryLayer()
            {
                Name = _realTimeLayerName,
                Features = kmoniObservationPoints,
                Style = null
            };
    }

    private IEnumerable<IFeature>? GetKmoniObservationPoints()
    {
        try
        {
            byte[] imageBytes = Task.Run(async () => await _imageFetch.GetByteArrayAsync(
                KmoniOptions.DataChoice,
                KmoniOptions.SensorChoice,
                _timeProvider.DateTimeOffsetNow().AddSeconds(-_kmoniDelaySeconds))).Result;

            using SKImage image = SKImage.FromEncodedData(imageBytes);
            using SKBitmap bm = SKBitmap.FromImage(image);
            using SKData data = bm.Encode(SKEncodedImageFormat.Png, 100);

            List<(ObservationPoint point, SKColor colour)> colours = _pointExtract.ExtractColours(bm, KmoniOptions.SensorChoice == KyoshinMonitor.Dto.Enum.SensorType.Borehole);
            List<(ObservationPoint point, double height)> heights = [];

            foreach ((ObservationPoint p, SKColor c) in colours)
            {
                heights.Add((p, ColourConversion.ColourToHeight(c)));
            }

            return heights.Select(pc =>
            {
                PointFeature feature = new(SphericalMercator.FromLonLat(pc.point.Location.Longitude, pc.point.Location.Latitude).ToMPoint());
                feature.Styles.Add(new SymbolStyle()
                {
                    SymbolScale = 0.1,
                    Fill = new Brush(ColourConversion.HeightToColour(pc.height).ToMapsui())
                });
                return feature;
            });
        }
        catch (AggregateException ae)
        {
            ae.Handle(ex =>
            {
                if (ex is HttpRequestException)
                {
                    Logger.TryGet(LogEventLevel.Warning, LogArea.Control)?.Log(this, $"HttpRequestException: {ex.Message}");
                }

                return ex is HttpRequestException;
            });
        }

        return null;
    }
    #endregion
}
