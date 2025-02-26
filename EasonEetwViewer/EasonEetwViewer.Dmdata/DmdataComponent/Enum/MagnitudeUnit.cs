using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.DmdataComponent.Enum;

/// <summary>
/// Describes the units of the magnitude.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<MagnitudeUnit>))]
public enum MagnitudeUnit
{
    /// <summary>
    /// The value <c>Mj</c>, representing JMA magnitude.
    /// </summary>
    [JsonStringEnumMemberName("Mj")]
    JmaMagnitude,
    /// <summary>
    /// The value <c>M</c>, representing normal magnitude.
    /// </summary>
    [JsonStringEnumMemberName("M")]
    NormalMagnitude
}
