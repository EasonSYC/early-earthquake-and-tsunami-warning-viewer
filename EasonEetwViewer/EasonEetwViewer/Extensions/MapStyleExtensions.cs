using EasonEetwViewer.Dmdata.Dtos.Enum;
using EasonEetwViewer.Dmdata.Telegram.Dtos.EarthquakeInformation;
using EasonEetwViewer.Dmdata.Telegram.Dtos.EewInformation;
using EasonEetwViewer.Dmdata.Telegram.Dtos.TsunamiInformation;
using EasonEetwViewer.KyoshinMonitor.Dtos;
using EasonEetwViewer.KyoshinMonitor.Extensions;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Projections;
using Mapsui.Rendering.Skia.Extensions;
using Mapsui.Styles;
using Mapsui.Styles.Thematics;
using SkiaSharp;
using Station = EasonEetwViewer.Dmdata.Api.Dtos.Record.EarthquakeParameter.Station;

namespace EasonEetwViewer.Extensions;

// Adapted from https://mapsui.com/v5/samples/ - Styles - ThemeStyle on ShapeFile
/// <summary>
/// Provides extensions to deal with <see cref="Mapsui"/>.
/// </summary>
internal static class MapStyleExtensions
{
    /// <summary>
    /// Creates a theme style for the regions.
    /// </summary>
    /// <param name="regions">The regions to be converted.</param>
    /// <returns>The converted style.</returns>
    public static ThemeStyle ToRegionStyle(this IEnumerable<RegionIntensity> regions)
        => new(feature =>
        {
            RegionIntensity? region = regions
                .SingleOrDefault(region => region.Code == (string)feature["code"]!);

            return region?.MaxInt is Intensity intensity
                ? new VectorStyle()
                {
                    Fill = new Brush(intensity.ToColourString().ToColour(0.60f))
                }
                : null;
        });
    /// <summary>
    /// Creates a theme style for the regions.
    /// </summary>
    /// <param name="regions">The regions to be converted.</param>
    /// <returns>The converted style.</returns>
    public static ThemeStyle ToRegionStyle(this IEnumerable<Region> regions)
        => new(feature =>
        {
            Region? region = regions
                .SingleOrDefault(region => region.Code == (string)feature["code"]!);
            return region?.ForecastMaxInt.From.ToIntensity() is Intensity intensity
                    ? new VectorStyle()
                    {
                        Fill = new Brush(intensity.ToColourString().ToColour(0.60f))
                    }
                    : null;
        });

    /// <summary>
    /// Creates a theme style for the regions.
    /// </summary>
    /// <param name="forecasts">The regions to be converted.</param>
    /// <returns>The converted style.</returns>
    public static ThemeStyle ToRegionStyle(this IEnumerable<Forecast> forecasts)
        => new(feature =>
        {
            Forecast? forecast = forecasts
                .SingleOrDefault(forecast => forecast.Code == (string)feature["code"]!);
            return forecast is null
                ? null
                : new VectorStyle()
                {
                    Line = new Pen(forecast.Kind.Code.ToTsunamiWarningType().ToColourString().ToColour(0.80f), 2.5)
                };
        });

    /// <summary>
    /// Creates a style for the intensity.
    /// </summary>
    /// <param name="intensity">The intensity for which the station has.</param>
    /// <returns>The converted style.</returns>
    public static SymbolStyle ToStationStyle(this Intensity intensity)
        => new()
        {
            SymbolScale = 0.25,
            Fill = new Brush(intensity.ToColourString().ToColour()),
            Outline = new Pen { Color = Color.Black }
        };

    /// <summary>
    /// Creates a style for the <see cref="SKColor"/> of the station.
    /// </summary>
    /// <param name="colour">The colour extracted from the station.</param>
    /// <returns>The style converted.</returns>
    public static SymbolStyle ToStationStyle(this SKColor colour)
        => new()
        {
            SymbolScale = 0.1,
            Fill = new Brush(colour.ColourToHeight().HeightToColour().ToMapsui())
        };

    /// <summary>
    /// Creates a feature for the station.
    /// </summary>
    /// <param name="pointColour">The tuple containing the station and the colour.</param>
    /// <returns>The converted feature.</returns>
    public static PointFeature ToStationFeature(this (ObservationPoint point, SKColor colour) pointColour)
        => new(
            SphericalMercator
            .FromLonLat(
                pointColour.point.Location.Longitude,
                pointColour.point.Location.Latitude)
            .ToMPoint())
        {
            Styles = [pointColour.colour.ToStationStyle()]
        };

    /// <summary>
    /// Creates a feature for the station.
    /// </summary>
    /// <param name="stationIntensity">The tuple containing the station and the intensity.</param>
    /// <returns>The converted feature.</returns>
    public static PointFeature ToStationFeature(this (Station station, Intensity intensity) stationIntensity)
        => new(
            SphericalMercator.FromLonLat(
                stationIntensity.station.Longitude,
                stationIntensity.station.Latitude)
            .ToMPoint())
        {
            Styles = [stationIntensity.intensity.ToStationStyle()]
        };

    /// <summary>
    /// Creates a rasterizing layer for the layer.
    /// </summary>
    /// <param name="layer">The layer to be included.</param>
    /// <returns>The rasterized layer.</returns>
    public static RasterizingLayer ToRasterizingLayer(this ILayer layer)
        => new(layer);

    /// <summary>
    /// Creates a rasterizing layer for the layer.
    /// </summary>
    /// <param name="layer">The layer to be included.</param>
    /// <returns>The rasterized layer.</returns>
    public static RasterizingLayer? ToNullableRasterizingLayer(this ILayer? layer)
        => layer is null
            ? null
            : new(layer);

    /// <summary>
    /// Adds the layer to the given <see cref="LayerCollection"/> if the layer is not null.
    /// </summary>
    /// <param name="collection">The layer collection for the layer to be added.</param>
    /// <param name="layer">The layer to be added.</param>
    /// <returns>The current instance of the collection for calls to be chained.</returns>
    public static LayerCollection AddIfNotNull(this LayerCollection collection, ILayer? layer)
    {
        if (layer is not null)
        {
            collection.Add(layer);
        }

        return collection;
    }
}
