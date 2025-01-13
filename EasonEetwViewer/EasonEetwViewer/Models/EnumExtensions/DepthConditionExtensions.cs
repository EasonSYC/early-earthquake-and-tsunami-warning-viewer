using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasonEetwViewer.HttpRequest.DmdataComponent.Enum;

namespace EasonEetwViewer.Models.EnumExtensions;
public static class DepthConditionExtensions
{
    public static string ToReadableString(this DepthCondition depthCondition) => depthCondition switch
    {
        DepthCondition.Deep => "700km以上",
        DepthCondition.Shallow => "ごく浅い",
        DepthCondition.Unclear => "不明",
        DepthCondition.Unknown or _ => "Unknown"
    };
}
