using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.DmdataComponent.Enum;

/// <summary>
/// Represents the source of information of a distant earthquake
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<Source>))]
public enum Source
{
    /// The value <c>ＵＳＧＳ</c>, representing the United States Geological Survey.
    /// </summary>
    [JsonStringEnumMemberName("ＵＳＧＳ")]
    USGS,
    /// <summary>
    /// The value <c>ＰＴＷＣ</c>, representing the Pacific Tsunami Warning Centre.
    /// </summary>
    [JsonStringEnumMemberName("ＰＴＷＣ")]
    PTWC,
    /// <summary>
    /// The value <c>ＳＣＳＴＡＣ</c>, representing the South China Sea Tsunami Advisory Centre.
    /// </summary>
    [JsonStringEnumMemberName("ＳＣＳＴＡＣ")]
    SCSTAC,
    /// <summary>
    /// The value <c>ＣＡＴＡＣ</c>, representing the Central America Tsunami Advisory Centre.
    /// </summary>
    [JsonStringEnumMemberName("ＣＡＴＡＣ")]
    CATAC,
    /// <summary>
    /// The value <c>ＷＣＡＴＷＣ</c>, representing the West Coast/Alaska Tsunami Warning Centre.
    /// </summary>
    [JsonStringEnumMemberName("ＷＣＡＴＷＣ")]
    WCATWC
}
