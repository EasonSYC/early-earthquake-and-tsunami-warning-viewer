using System.Timers;
using Avalonia.Logging;
using EasonEetwViewer.Authentication;
using EasonEetwViewer.HttpRequest.Caller;
using EasonEetwViewer.KyoshinMonitor;
using EasonEetwViewer.KyoshinMonitor.Dto;
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


    internal KmoniOptions KmoniOptions { get; init; }
    private const int _jstAheadUtcHours = 9;
    internal static string TimeDisplayText => DateTimeOffset.Now.ToOffset(new(_jstAheadUtcHours, 0, 0)).ToString("yyyy/MM/dd HH:mm:ss");

    private readonly System.Timers.Timer _timer;
    public RealtimePageViewModel(ImageFetch imageFetch, PointExtract pointExtract, System.Timers.Timer timer, StaticResources resources, KmoniOptions kmoniOptions, AuthenticatorDto authenticatorDto, IApiCaller apiCaller, ITelegramRetriever telegramRetriever, OnAuthenticatorChanged onChange)
    : base(resources, authenticatorDto, apiCaller, telegramRetriever, onChange)
    {
        KmoniOptions = kmoniOptions;
        _imageFetch = imageFetch;
        _pointExtract = pointExtract;
        _timer = timer;
        _timer.Elapsed += OnTimedEvent;
    }

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
                IsMapInfoLayer = true,
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
}
