using System.Diagnostics;
using EasonEetwViewer.Dmdata.Dtos.Enum;
using EasonEetwViewer.Lang;
using EasonEetwViewer.Models.RealTimePage;

namespace EasonEetwViewer.Extensions;

/// <summary>
/// Provides extension methods for enums to have <c>ToColourString</c> method.
/// </summary>
internal static class EnumDisplayColourExtensions
{
    /// <summary>
    /// Converts <see cref="TsunamiWarningType"/> to colour string.
    /// </summary>
    /// <param name="tsunamiWarningType">The enum to be converted.</param>
    /// <returns>The string representing the colour.</returns>
    /// <exception cref="UnreachableException">When the program reaches an unreachable state.</exception>
    public static string ToColourString(this TsunamiWarningType tsunamiWarningType)
        => tsunamiWarningType switch
        {
            TsunamiWarningType.Information
                => RealtimePageResources.TsunamiColourInformation,
            TsunamiWarningType.Caution
                => RealtimePageResources.TsunamiColourCaution,
            TsunamiWarningType.Warning
                => RealtimePageResources.TsunamiColourWarning,
            TsunamiWarningType.SpecialWarning
                => RealtimePageResources.TsunamiColourSpecialWarning,
            TsunamiWarningType.None
                => RealtimePageResources.TsunamiColourNone,
            _
                => throw new UnreachableException()
        };

    /// <summary>
    /// Converts <see cref="Intensity"/> to colour string.
    /// </summary>
    /// <param name="intensity">The enum to be converted.</param>
    /// <returns>The string representing the colour.</returns>
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
