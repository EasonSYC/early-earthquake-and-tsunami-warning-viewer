using System.Timers;
using Avalonia.Logging;
using EasonEetwViewer.KyoshinMonitor;
using EasonEetwViewer.KyoshinMonitor.Dto;
using EasonEetwViewer.Models;
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
    public RealtimePageViewModel(UserOptions options) : base(options)
    {
        _imageFetch = new();
        _pointExtract = new KmoniPointExtract("ObservationPoints.json");

        _kmoniLayer = GetKmoniLayer();
        if (_kmoniLayer is not null)
        {
            Map.Layers.Add(_kmoniLayer);
        }

        _timer = new(1000);
        _timer.Elapsed += OnTimedEvent;
        _timer.AutoReset = true;
        _timer.Enabled = true;
    }

    private const string _realTimeLayerName = "KmoniLayer";
    private const int _kmoniDelaySeconds = 1;
    private readonly KmoniImageFetch _imageFetch;
    private readonly KmoniPointExtract _pointExtract;

    private ILayer? _kmoniLayer;
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

    private ILayer? GetKmoniLayer()
    {
        IEnumerable<IFeature>? kmoniObservationPoints = GetKmoniObservationPoints();
        return kmoniObservationPoints is null
            ? null
            : new Layer()
            {
                Name = _realTimeLayerName,
                DataSource = new MemoryProvider(kmoniObservationPoints),
                IsMapInfoLayer = true,
                Style = null
            };
    }

    private IEnumerable<IFeature>? GetKmoniObservationPoints()
    {
        try
        {
            byte[] imageBytes = Task.Run(async () => await _imageFetch.GetByteArrayAsync(
                Options.DataChoice.Item1,
                Options.SensorChoice.Item1,
                DateTime.UtcNow.AddSeconds(-_kmoniDelaySeconds))).Result;

            using SKImage image = SKImage.FromEncodedData(imageBytes);
            using SKBitmap bm = SKBitmap.FromImage(image);

            using SKData data = bm.Encode(SKEncodedImageFormat.Png, 100);

            List<(ObservationPoint point, SKColor colour)> colours = _pointExtract.ExtractColours(bm, Options.SensorChoice.Item1 == KyoshinMonitor.Dto.Enum.SensorType.Borehole);
            List<(ObservationPoint point, double intensity)> intensities = [];

            foreach ((ObservationPoint p, SKColor c) in colours)
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
        catch (ArgumentException ex)
        {
            Logger.TryGet(LogEventLevel.Warning, LogArea.Control)?.Log(this, $"ArgumentException: {ex.Message}");
        }

        return null;
    }
}
