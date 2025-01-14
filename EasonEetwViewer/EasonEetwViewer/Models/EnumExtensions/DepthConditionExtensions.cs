using EasonEetwViewer.HttpRequest.DmdataComponent.Enum;

namespace EasonEetwViewer.Models.EnumExtensions;
public static class DepthConditionExtensions
{
    public static string ToReadableString(this DepthCondition depthCondition) => depthCondition switch
    {
        DepthCondition.Deep => "700km+",
        DepthCondition.Shallow => "Shallow",
        DepthCondition.Unclear => "Unclear",
        DepthCondition.Unknown or _ => "Unknown"
    };
}
