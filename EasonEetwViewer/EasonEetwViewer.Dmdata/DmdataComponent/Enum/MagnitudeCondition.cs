using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.DmdataComponent.Enum;

/// <summary>
/// Describes the special situations of the magnitude value.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<MagnitudeCondition>))]
public enum MagnitudeCondition
{
    /// <summary>
    /// Unknown. Default value.
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// The value <c>Ｍ不明</c>, representing unclear.
    /// </summary>
    [JsonStringEnumMemberName("Ｍ不明")]
    Unclear = 1,
    /// <summary>
    /// The value <c>Ｍ８を超える巨大地震</c>, representing too big an earthquake.
    /// </summary>
    [JsonStringEnumMemberName("Ｍ８を超える巨大地震")]
    Huge = 2
}
