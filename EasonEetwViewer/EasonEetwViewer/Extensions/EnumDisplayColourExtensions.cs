using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasonEetwViewer.Dmdata.Dtos.Enum;
using EasonEetwViewer.Lang;
using EasonEetwViewer.Models.RealTimePage;

namespace EasonEetwViewer.Extensions;

internal static class EnumDisplayColourExtensions
{
    /// <summary>
    /// Converts <see cref="TsunamiWarningType"/> to colour string.
    /// </summary>
    /// <param name="tsunamiWarningType">The tsunami waarning type to be converted.</param>
    /// <returns>The string representing the colour for the tsunami warning type.</returns>
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
