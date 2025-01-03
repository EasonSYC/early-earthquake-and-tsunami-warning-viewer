using System.Timers;
using Avalonia.Logging;
using EasonEetwViewer.KyoshinMonitor;
using EasonEetwViewer.KyoshinMonitor.Dto;
using EasonEetwViewer.KyoshinMonitor.Dto.Enum;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Projections;
using Mapsui.Providers;
using Mapsui.Rendering.Skia.Extensions;
using Mapsui.Styles;
using SkiaSharp;

namespace EasonEetwViewer.ViewModels;

internal partial class RealtimePageViewModel : MapViewModelBase
{
    private const int _jstAheadUtcHours = 9;
    internal static string TimeDisplayText => DateTime.UtcNow.AddHours(_jstAheadUtcHours).ToString("yyyy/MM/dd HH:mm:ss");

    private readonly System.Timers.Timer _timer;
    public RealtimePageViewModel() : base()
    {
        _imageFetch = new();
        _pointExtract = new KmoniPointExtract("ObservationPoints.json");

        _kmoniLayer = GetKmoniLayer();
        Map.Layers.Add(_kmoniLayer);

        _timer = new(1000);
        _timer.Elapsed += OnTimedEvent;
        _timer.AutoReset = true;
        _timer.Enabled = true;
    }

    private const string _layerName = "KmoniLayer";
    private KmoniImageFetch _imageFetch;
    private KmoniPointExtract _pointExtract;

    private ILayer _kmoniLayer;
    private void OnTimedEvent(object? source, ElapsedEventArgs e)
    {
        OnPropertyChanged(nameof(TimeDisplayText));
        ILayer? newLayer = GetKmoniLayer();
        if (newLayer is not null)
        {
            _ = Map.Layers.Remove(_kmoniLayer);
            _kmoniLayer = newLayer;
            Map.Layers.Add(_kmoniLayer);
        }
    }

    // Adapted from https://mapsui.com/samples/ - Info - Custom Callout

    private ILayer? GetKmoniLayer()
    {
        IEnumerable<IFeature>? kmoniObservationPoints = GetKmoniObservationPoints();
        return kmoniObservationPoints is null
            ? null
            : new Layer()
            {
                Name = _layerName,
                DataSource = new MemoryProvider(kmoniObservationPoints),
                IsMapInfoLayer = true,
                Style = null
            };
    }

    private IEnumerable<IFeature>? GetKmoniObservationPoints()
    {
        try
        {
            byte[] imageBytes = Task.Run(async () => await _imageFetch.GetByteArrayAsync(KmoniDataType.MeasuredIntensity, SensorType.Surface, DateTime.UtcNow)).Result;

            using SkiaSharp.SKImage image = SkiaSharp.SKImage.FromEncodedData(imageBytes);
            using SKBitmap bm = SKBitmap.FromImage(image);

            using SKData data = bm.Encode(SKEncodedImageFormat.Png, 100);

            List<(ObservationPoint point, SkiaSharp.SKColor colour)> colours = _pointExtract.ExtractColours(bm);
            List<(ObservationPoint point, double intensity)> intensities = [];

            foreach ((ObservationPoint p, SkiaSharp.SKColor c) in colours)
            {
                intensities.Add((p, KmoniColourConversion.HeightToIntensity(KmoniColourConversion.ColourToHeight(c))));
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

        return null;
    }
}
