using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dtos.DmdataComponent.Enum;

/// <summary>
/// Describes the special situations of the depth value.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<DepthCondition>))]
public enum DepthCondition
{
    /// <summary>
    /// The value <c>ごく浅い</c>, representing too shallow.
    /// </summary>
    [JsonStringEnumMemberName("ごく浅い")]
    Shallow,
    /// <summary>
    /// The value <c>７００ｋｍ以上</c>, representing too deep.
    /// </summary>
    [JsonStringEnumMemberName("７００ｋｍ以上")]
    Deep,
    /// <summary>
    /// The value <c>不明</c>, representing unclear.
    /// </summary>
    [JsonStringEnumMemberName("不明")]
    Unclear
}
