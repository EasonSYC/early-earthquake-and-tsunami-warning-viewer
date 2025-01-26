using System.Collections.ObjectModel;
using System.Text.Json;
using System.Timers;
using Avalonia.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using EasonEetwViewer.Authentication;
using EasonEetwViewer.Dmdata.Caller.Interfaces;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Enum.WebSocket;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.Schema;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.TelegramBase;
using EasonEetwViewer.KyoshinMonitor;
using EasonEetwViewer.KyoshinMonitor.Dto;
using EasonEetwViewer.Models;
using EasonEetwViewer.Services;
using EasonEetwViewer.Services.KmoniOptions;
using EasonEetwViewer.ViewModels.ViewModelBases;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Projections;
using Mapsui.Rendering.Skia.Extensions;
using Mapsui.Styles;
using SkiaSharp;

namespace EasonEetwViewer.ViewModels;

internal partial class RealtimePageViewModel : MapViewModelBase
{
    public RealtimePageViewModel(ImageFetch imageFetch, PointExtract pointExtract, StaticResources resources, KmoniOptions kmoniOptions, AuthenticatorDto authenticatorDto, IApiCaller apiCaller, ITelegramRetriever telegramRetriever, IWebSocketClient webSocketClient, OnAuthenticatorChanged onChange)
    : base(resources, authenticatorDto, apiCaller, telegramRetriever, onChange)
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

        _cts = new();
        _token = _cts.Token;

        Task.Run(StartLongRunning).Wait();
    }

    private async Task StartLongRunning()
    {
        _ = await Task.Factory.StartNew(SwitchEew, _token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        _ = await Task.Factory.StartNew(DrawEewCircles, _token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        _ = await Task.Factory.StartNew(ClearExpiredEew, _token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
    }

    private readonly IWebSocketClient _webSocketClient;
    private readonly CancellationTokenSource _cts;
    private readonly CancellationToken _token;

    private const string _eewHypocentreLayerPrefix = "Hypocentre";
    private const string _eewRegionLayerPrefix = "Regions";
    private const string _eewWavefrontLayerPrefix = "Wavefront";

    private readonly TimeSpan _refreshCircleInterval = TimeSpan.FromMilliseconds(500);
    private readonly TimeSpan _switchEewInterval = TimeSpan.FromSeconds(2);
    private readonly TimeSpan _removeExpiredEewInterval = TimeSpan.FromSeconds(2);

    #region eew
    [ObservableProperty]
    private ObservableCollection<EewDetailsTemplate> _liveEewList = [];
    public EewDetailsTemplate? CurrentDisplayEew
        => CurrentEewIndex is null || CurrentEewIndex >= LiveEewList.Count
            ? null
            : LiveEewList[(int)CurrentEewIndex];
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentDisplayEew))]
    private uint? _currentEewIndex = null;
    private void OnEewReceived(EewInformationSchema eew)
    {
        for (int i = LiveEewList.Count - 1; i >= 0; --i)
        {
            if (LiveEewList[i].EventId == eew.EventId)
            {
                LiveEewList.RemoveAt(i);
                break;
            }
        }

        // Add the new eew to the list by leaving only the details to be displayed
        // LiveEewList.Add();

        // Add the layer for the hypocentre
        // Map.Layers.Add;

        // Add the layer for the regions
        // Map.Layers.Add;
    }
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
    private async Task DrawEewCircles()
    {
        while (!_token.IsCancellationRequested)
        {

            await Task.Delay(_refreshCircleInterval);
        }
    }
    private async Task ClearExpiredEew()
    {
        while (!_token.IsCancellationRequested)
        {
            for (int i = LiveEewList.Count - 1; i >= 0; --i)
            {
                if (LiveEewList[i].ExpiryTime < DateTimeOffset.Now)
                {
                    RemoveEew(LiveEewList[i]);
                }
            }

            await Task.Delay(_removeExpiredEewInterval);
        }
    }
    private void RemoveEew(EewDetailsTemplate eew)
    {
        if (LiveEewList.Remove(eew))
        {
            _ = Map.Layers.Remove(x => x.Name == (_eewHypocentreLayerPrefix + eew.EventId));
            _ = Map.Layers.Remove(x => x.Name == (_eewRegionLayerPrefix + eew.EventId));
            _ = Map.Layers.Remove(x => x.Name == (_eewWavefrontLayerPrefix + eew.EventId));
        }
    }
    #endregion

    private const string _tsunamiWarningLayerName = "Tsunami";
    #region tsunami
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
                EewInformationSchema eew = JsonSerializer.Deserialize<EewInformationSchema>(data)!;
                OnEewReceived(eew);
                return;
            }

            if (headData.Schema.Type == "tsunami-information" && headData.Schema.Version == "1.1.0")
            {
                TsunamiInformationSchema tsunami = JsonSerializer.Deserialize<TsunamiInformationSchema>(data)!;
                OnTsunamiReceived(tsunami);
                return;
            }
        }
    }
    #endregion

    #region kmoni
    internal KmoniOptions KmoniOptions { get; init; }

    private const int _jstAheadUtcHours = 9;
    internal static string TimeDisplayText => DateTimeOffset.Now.ToOffset(new(_jstAheadUtcHours, 0, 0)).ToString("yyyy/MM/dd HH:mm:ss");
    private readonly System.Timers.Timer _timer;

    private const string _realTimeLayerName = "KmoniLayer";
    private const int _kmoniDelaySeconds = 1;
    private readonly ImageFetch _imageFetch;
    private readonly PointExtract _pointExtract;

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
                DateTimeOffset.Now.AddSeconds(-_kmoniDelaySeconds))).Result;

            using SKImage image = SKImage.FromEncodedData(imageBytes);
            using SKBitmap bm = SKBitmap.FromImage(image);

            using SKData data = bm.Encode(SKEncodedImageFormat.Png, 100);

            List<(ObservationPoint point, SKColor colour)> colours = _pointExtract.ExtractColours(bm, KmoniOptions.SensorChoice == KyoshinMonitor.Dto.Enum.SensorType.Borehole);
            List<(ObservationPoint point, double intensity)> intensities = [];

            foreach ((ObservationPoint p, SKColor c) in colours)
            {
                intensities.Add((p, ColourConversion.HeightToIntensity(ColourConversion.ColourToHeight(c))));
            }

            return colours.Select(pc =>
            {
                PointFeature feature = new(SphericalMercator.FromLonLat(pc.point.Location.Longitude, pc.point.Location.Latitude).ToMPoint());
                feature.Styles.Add(new SymbolStyle()
                {
                    SymbolScale = 0.2,
                    Fill = new Brush(pc.colour.ToMapsui())
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
        catch (ArgumentException ex)
        {
            Logger.TryGet(LogEventLevel.Warning, LogArea.Control)?.Log(this, $"ArgumentException: {ex.Message}");
        }

        return null;
    }
    #endregion
}
