using EasonEetwViewer.HttpRequest.Dto.Enum;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Services;
public static class EarthquakeIntensityExtensions
{
    public static string ToColourString(this EarthquakeIntensity intensity) => intensity switch
    {
        EarthquakeIntensity.One => Resources.EarthquakeIntensityOneColour,
        EarthquakeIntensity.Two => Resources.EarthquakeIntensityTwoColour,
        EarthquakeIntensity.Three => Resources.EarthquakeIntensityThreeColour,
        EarthquakeIntensity.Four => Resources.EarthquakeIntensityFourColour,
        EarthquakeIntensity.FiveWeak => Resources.EarthquakeIntensityFiveWeakColour,
        EarthquakeIntensity.FiveStrong => Resources.EarthquakeIntensityFiveStrongColour,
        EarthquakeIntensity.SixWeak => Resources.EarthquakeIntensitySixWeakColour,
        EarthquakeIntensity.SixStrong => Resources.EarthquakeIntensitySixStrongColour,
        EarthquakeIntensity.Seven => Resources.EarthquakeIntensitySevenColour,
        EarthquakeIntensity.Unknown or _ => Resources.EarthquakeIntensityUnknownColour
    };
}
