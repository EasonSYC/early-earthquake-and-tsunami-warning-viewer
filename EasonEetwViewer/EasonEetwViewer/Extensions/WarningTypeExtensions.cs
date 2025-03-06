using System.Diagnostics;
using EasonEetwViewer.Dmdata.Telegram.Dtos.EewInformation;
using EasonEetwViewer.Dmdata.Telegram.Dtos.EewInformation.Enum.Accuracy;
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

    /// <summary>
    /// Determines if the accuracy is a one-point information.
    /// </summary>
    /// <param name="accuracy">The accuracy to be determined.</param>
    /// <returns>Whether it is a one-point information.</returns>
    public static bool IsOnePointInfo(this Accuracy accuracy)
        => accuracy.Epicentres[0] == EpicentreDepth.LevelIpf1Plum
            && accuracy.Epicentres[1] == EpicentreDepth.LevelIpf1Plum
            && accuracy.Depth == EpicentreDepth.LevelIpf1Plum
            && accuracy.Magnitude == Magnitude.LevelOrPlum
            && accuracy.MagnitudePoint == MagnitudePoint.OneOrLevelOrPlum;

    /// <summary>
    /// Determines if the earthquake has an assumed hypocentre.
    /// </summary>
    /// <param name="earthquake">The earthquake to be determined.</param>
    /// <returns>Whether it has an assumed hypocentre.</returns>
    public static bool IsAssumedHypocentre(this Earthquake earthquake)
        => earthquake.Condition is "仮定震源要素";
}
