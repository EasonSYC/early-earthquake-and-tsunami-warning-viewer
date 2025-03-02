using System.Diagnostics;
using EasonEetwViewer.Dmdata.Dtos.Enum;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Extensions;
/// <summary>
/// Provides extension methods for <see cref="Intensity"/> to convert to colour string..
/// </summary>
internal static class EarthquakeIntensityColourExtensions
{
    /// <summary>
    /// Converts <see cref="Intensity"/> to colour string.
    /// </summary>
    /// <param name="intensity">The intensity to be converted.</param>
    /// <returns>The string representing the colour for the intensity.</returns>
    /// <exception cref="UnreachableException">When the program reaches an unreachable state.</exception>
    public static string ToColourString(this Intensity intensity)
        => intensity switch
        {
            Intensity.One
                => Resources.EarthquakeIntensityOneColour,
            Intensity.Two
                => Resources.EarthquakeIntensityTwoColour,
            Intensity.Three
                => Resources.EarthquakeIntensityThreeColour,
            Intensity.Four
                => Resources.EarthquakeIntensityFourColour,
            Intensity.FiveWeak
                => Resources.EarthquakeIntensityFiveWeakColour,
            Intensity.FiveStrong
                => Resources.EarthquakeIntensityFiveStrongColour,
            Intensity.SixWeak
                => Resources.EarthquakeIntensitySixWeakColour,
            Intensity.SixStrong
                => Resources.EarthquakeIntensitySixStrongColour,
            Intensity.Seven
                => Resources.EarthquakeIntensitySevenColour,
            _
                => throw new UnreachableException()
        };
}
