using EasonEetwViewer.Dmdata.Dtos.Enum;
using EasonEetwViewer.Dmdata.Telegram.Dtos.EarthquakeInformation;
using EasonEetwViewer.Dmdata.Telegram.Dtos.EewInformation;
using EasonEetwViewer.Dmdata.Telegram.Dtos.TsunamiInformation;
using EasonEetwViewer.KyoshinMonitor.Dtos;
using EasonEetwViewer.KyoshinMonitor.Extensions;
using Mapsui;
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
        => new((pointColour.point.Location.Longitude, pointColour.point.Location.Latitude).LonLatToMPoint())
        {
            Styles = [pointColour.colour.ToStationStyle()]
        };

    /// <summary>
    /// Creates a feature for the station.
    /// </summary>
    /// <param name="stationIntensity">The tuple containing the station and the intensity.</param>
    /// <returns>The converted feature.</returns>
    public static PointFeature ToStationFeature(this (Station station, Intensity intensity) stationIntensity)
        => new((stationIntensity.station.Longitude, stationIntensity.station.Latitude).LonLatToMPoint())
        {
            Styles = [stationIntensity.intensity.ToStationStyle()]
        };

    /// <summary>
    /// Converts a pair of longitude and latitude to a <see cref="MPoint"/>.
    /// </summary>
    /// <param name="coordinate">The pair of coordinates to convert.</param>
    /// <returns>The converted <see cref="MPoint"/>.</returns>
    public static MPoint LonLatToMPoint(this (double longitude, double latitude) coordinate)
     => SphericalMercator
        .FromLonLat(
            coordinate.longitude,
            coordinate.latitude)
        .ToMPoint();

    /// <summary>
    /// Round a double to the certain number of significant figures, in decimal.
    /// </summary>
    /// <param name="value">The value to be rounded.</param>
    /// <param name="figures">The number of significant figures desired.</param>
    /// <returns>The decimal with the desired number of significant figures.</returns>
    public static decimal ToSignificantFigures(this double value, int figures)
    {
        if (value == 0)
        {
            return 0;
        }

        decimal d = (decimal)value;
        int digits = (int)Math.Floor(Math.Log10(Math.Abs(value)) + 1);

        decimal scaleFactor = (decimal)Math.Pow(10, digits);
        return scaleFactor * Math.Round(d / scaleFactor, figures);
    }
}
