using System.Diagnostics;
using EasonEetwViewer.Lang;
using EasonEetwViewer.Models.RealTimePage;

namespace EasonEetwViewer.Extensions;
/// <summary>
/// Provides extension methods for <see cref="TsunamiWarningType"/> to convert.
/// </summary>
internal static class TsunamiWarningTypeExtensions
{
    /// <summary>
    /// Converts the code string to a tsunami warning type.
    /// </summary>
    /// <param name="code">The code to be converted.</param>
    /// <returns>The converted tsunami warning type.</returns>
    /// <exception cref="UnreachableException">When the program reaches an unreachable state.</exception>
    public static TsunamiWarningType ToTsunamiWarningType(this string code)
        => code switch
        {
            "00" or "50" or "60"
                => TsunamiWarningType.None,
            "71" or "72" or "73"
                => TsunamiWarningType.Information,
            "62"
                => TsunamiWarningType.Caution,
            "51"
                => TsunamiWarningType.Warning,
            "52" or "53"
                => TsunamiWarningType.SpecialWarning,
            _
                => throw new UnreachableException()
        };
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
}
