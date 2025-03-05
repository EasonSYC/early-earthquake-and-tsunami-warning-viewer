using EasonEetwViewer.Dmdata.Api.Dtos.Record.EarthquakeParameter;
using EasonEetwViewer.Dmdata.Dtos.Enum;
using EasonEetwViewer.Dmdata.Telegram.Dtos.EarthquakeInformation;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Projections;
using Mapsui.Styles;
using Mapsui.Styles.Thematics;

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
        => new(f =>
        {
            RegionIntensity? region = regions
                .SingleOrDefault(r => r.Code == (string)f["code"]!);

            return region?.MaxInt is Intensity intensity
                ? new VectorStyle()
                {
                    Fill = new Brush(intensity.ToColourString().ToColour(0.60f))
                }
                : null;
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
