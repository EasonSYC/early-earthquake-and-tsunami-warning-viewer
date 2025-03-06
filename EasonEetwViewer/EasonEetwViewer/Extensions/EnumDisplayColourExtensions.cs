using System.Diagnostics;
using EasonEetwViewer.Dmdata.Dtos.Enum;
using EasonEetwViewer.Dmdata.Telegram.Dtos.EewInformation.Enum.Range;
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

    /// <summary>
    /// Converts <see cref="EewWarningType"/> to colour string.
    /// </summary>
    /// <param name="eew">The enum to be converted.</param>
    /// <returns>The string representing the colour.</returns>
    /// <exception cref="UnreachableException">When the program reaches an unreachable state.</exception>
    public static string ToColourString(this EewWarningType eew)
        => eew switch
        {
            EewWarningType.Cancelled
                => RealtimePageResources.EewColourCancelled,
            EewWarningType.Final
                => RealtimePageResources.EewColourFinal,
            EewWarningType.Warning
                => RealtimePageResources.EewColourWarning,
            EewWarningType.Forecast
                => RealtimePageResources.EewColourForecast,
            _
                => throw new UnreachableException()
        };

    /// <summary>
    /// Converts <see cref="IntensityLower"/> to colour string.
    /// </summary>
    /// <param name="intensity">The enum to be converted.</param>
    /// <returns>The string representing the colour.</returns>
    /// <exception cref="UnreachableException">When the program reaches an unreachable state.</exception>
    public static string ToColourString(this IntensityLower intensity)
        => intensity switch
        {
            IntensityLower.Zero
                => EarthquakeResources.IntensityColourZero,
            IntensityLower.One
                => EarthquakeResources.IntensityColourOne,
            IntensityLower.Two
                => EarthquakeResources.IntensityColourTwo,
            IntensityLower.Three
                => EarthquakeResources.IntensityColourThree,
            IntensityLower.Four
                => EarthquakeResources.IntensityColourFour,
            IntensityLower.FiveWeak
                => EarthquakeResources.IntensityColourFiveWeak,
            IntensityLower.FiveStrong
                => EarthquakeResources.IntensityColourFiveStrong,
            IntensityLower.SixWeak
                => EarthquakeResources.IntensityColourSixWeak,
            IntensityLower.SixStrong
                => EarthquakeResources.IntensityColourSixStrong,
            IntensityLower.Seven
                => EarthquakeResources.IntensityColourSeven,
            IntensityLower.Unclear
                => EarthquakeResources.IntensityColourUnknown,
            _
                => throw new UnreachableException()
        };

    /// <summary>
    /// Converts <see cref="IntensityUpper"/> to colour string.
    /// </summary>
    /// <param name="intensity">The enum to be converted.</param>
    /// <returns>The string representing the colour.</returns>
    /// <exception cref="UnreachableException">When the program reaches an unreachable state.</exception>
    public static string ToColourString(this IntensityUpper intensity)
        => intensity switch
        {
            IntensityUpper.Zero
                => EarthquakeResources.IntensityColourZero,
            IntensityUpper.One
                => EarthquakeResources.IntensityColourOne,
            IntensityUpper.Two
                => EarthquakeResources.IntensityColourTwo,
            IntensityUpper.Three
                => EarthquakeResources.IntensityColourThree,
            IntensityUpper.Four
                => EarthquakeResources.IntensityColourFour,
            IntensityUpper.FiveWeak
                => EarthquakeResources.IntensityColourFiveWeak,
            IntensityUpper.FiveStrong
                => EarthquakeResources.IntensityColourFiveStrong,
            IntensityUpper.SixWeak
                => EarthquakeResources.IntensityColourSixWeak,
            IntensityUpper.SixStrong
                => EarthquakeResources.IntensityColourSixStrong,
            IntensityUpper.Seven
                => EarthquakeResources.IntensityColourSeven,
            IntensityUpper.Unclear or IntensityUpper.Above
                => EarthquakeResources.IntensityColourUnknown,
            _
                => throw new UnreachableException()
        };
}
