using System.Diagnostics;
using EasonEetwViewer.Dmdata.Telegram.Dtos.Schema;
using EasonEetwViewer.Models.RealTimePage;

namespace EasonEetwViewer.Extensions;
/// <summary>
/// Provides extension methods for warning type conversion.
/// </summary>
internal static class WarningTypeExtensions
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
    /// Converts the EEW Information Schema to an EEW Warning Type.
    /// </summary>
    /// <param name="eew">The EEW to be converted.</param>
    /// <returns>The converted EEW Warning Type.</returns>
    public static EewWarningType ToWarningType(this EewInformationSchema eew)
        => eew.Body.IsCancelled
            ? EewWarningType.Cancelled
            : eew.Body.IsLastInfo
                ? EewWarningType.Final
                : eew.Body.IsWarning ?? false
                    ? EewWarningType.Warning
                    : EewWarningType.Forecast;
}
