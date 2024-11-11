using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.Http.Response.Enum;

/// <summary>
/// Represents the source of information of a distant earthquake
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<Source>))]
public enum Source
{
    /// <summary>
    /// Unknown. Default value.
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// The value <c>ＵＳＧＳ</c>, representing the United States Geological Survey.
    /// </summary>
    [JsonStringEnumMemberName("ＵＳＧＳ")]
    USGS = 1,
    /// <summary>
    /// The value <c>ＰＴＷＣ</c>, representing the Pacific Tsunami Warning Centre.
    /// </summary>
    [JsonStringEnumMemberName("ＰＴＷＣ")]
    PTWC = 2,
    /// <summary>
    /// The value <c>ＳＣＳＴＡＣ</c>, representing the South China Sea Tsunami Advisory Centre.
    /// </summary>
    [JsonStringEnumMemberName("ＳＣＳＴＡＣ")]
    SCSTAC = 3,
    /// <summary>
    /// The value <c>ＣＡＴＡＣ</c>, representing the Central America Tsunami Advisory Centre.
    /// </summary>
    [JsonStringEnumMemberName("ＣＡＴＡＣ")]
    CATAC = 4,
    /// <summary>
    /// The value <c>ＷＣＡＴＷＣ</c>, representing the West Coast/Alaska Tsunami Warning Centre.
    /// </summary>
    [JsonStringEnumMemberName("ＷＣＡＴＷＣ")]
    WCATWC = 5
}
