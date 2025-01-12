using System.Text.Json.Serialization;

namespace EasonEetwViewer.HttpRequest.DmdataComponent.Enum;

/// <summary>
/// Describes the units of the magnitude.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<MagnitudeUnit>))]
public enum MagnitudeUnit
{
    /// <summary>
    /// Unknown. Default value.
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// The value <c>Mj</c>, representing JMA magnitude.
    /// </summary>
    [JsonStringEnumMemberName("Mj")]
    JmaMagnitude = 1,
    /// <summary>
    /// The value <c>M</c>, representing normal magnitude.
    /// </summary>
    [JsonStringEnumMemberName("M")]
    NormalMagnitude = 2
}
