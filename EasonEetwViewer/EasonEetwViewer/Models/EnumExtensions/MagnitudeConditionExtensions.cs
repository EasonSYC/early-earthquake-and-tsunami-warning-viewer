using EasonEetwViewer.HttpRequest.DmdataComponent.Enum;

public static class MagnitudeConditionExtensions
{
    public static string ToReadableString(this MagnitudeCondition magnitudeCondition) => magnitudeCondition switch
    {
        MagnitudeCondition.Huge => "8+",
        MagnitudeCondition.Unclear => "不明",
        MagnitudeCondition.Unknown or _ => "Unknown"
    };
}
