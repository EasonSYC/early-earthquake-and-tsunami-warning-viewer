using System.Diagnostics;
using EasonEetwViewer.Dtos.Dto.ApiResponse.Enum;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Services;
public static class EarthquakeIntensityExtensions
{
    public static string ToColourString(this Intensity intensity) => intensity switch
    {
        Intensity.One => Resources.EarthquakeIntensityOneColour,
        Intensity.Two => Resources.EarthquakeIntensityTwoColour,
        Intensity.Three => Resources.EarthquakeIntensityThreeColour,
        Intensity.Four => Resources.EarthquakeIntensityFourColour,
        Intensity.FiveWeak => Resources.EarthquakeIntensityFiveWeakColour,
        Intensity.FiveStrong => Resources.EarthquakeIntensityFiveStrongColour,
        Intensity.SixWeak => Resources.EarthquakeIntensitySixWeakColour,
        Intensity.SixStrong => Resources.EarthquakeIntensitySixStrongColour,
        Intensity.Seven => Resources.EarthquakeIntensitySevenColour,
        _ => throw new UnreachableException()
    };
}
