using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasonEetwViewer.Lang;
using EasonEetwViewer.Models;

namespace EasonEetwViewer.Services;
internal static class TsunamiWarningTypeExtensions
{
    public static TsunamiWarningType ToTsunamiWarningType(this string code)
        => code switch
        {
            "00" or "50" or "60" => TsunamiWarningType.None,
            "51" => TsunamiWarningType.Warning,
            "52" or "53" => TsunamiWarningType.SpecialWarning,
            "62" => TsunamiWarningType.Caution,
            "71" or "72" or "73" => TsunamiWarningType.Information,
            _ => TsunamiWarningType.None
        };
    public static string ToColourString(this TsunamiWarningType tsunamiWarningType)
        => tsunamiWarningType switch
        {
            TsunamiWarningType.Information => RealtimePageResources.TsunamiColourInformation,
            TsunamiWarningType.Caution => RealtimePageResources.TsunamiColourCaution,
            TsunamiWarningType.Warning => RealtimePageResources.TsunamiColourWarning,
            TsunamiWarningType.SpecialWarning => RealtimePageResources.TsunamiColourSpecialWarning,
            TsunamiWarningType.None or _ => RealtimePageResources.TsunamiColourNone,
        };
}
