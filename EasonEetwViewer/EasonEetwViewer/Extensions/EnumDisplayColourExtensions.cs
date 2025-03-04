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
                => EarthquakeResources.IntensityColourOne,
            Intensity.Two
                => EarthquakeResources.IntensityColourTwo,
            Intensity.Three
                => EarthquakeResources.IntensityColourThree,
            Intensity.Four
                => EarthquakeResources.IntensityColourFour,
            Intensity.FiveWeak
                => EarthquakeResources.IntensityColourFiveWeak,
            Intensity.FiveStrong
                => EarthquakeResources.IntensityColourFiveStrong,
            Intensity.SixWeak
                => EarthquakeResources.IntensityColourSixWeak,
            Intensity.SixStrong
                => EarthquakeResources.IntensityColourSixStrong,
            Intensity.Seven
                => EarthquakeResources.IntensityColourSeven,
            _
                => throw new UnreachableException()
        };
}
