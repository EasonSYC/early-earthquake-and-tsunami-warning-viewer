using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dtos.DmdataComponent.Enum;

/// <summary>
/// Represents the geodetic system used by a pair of coordinates.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<Geodetic>))]
public enum Geodetic
{
    /// <summary>
    /// The value <c>世界測地系</c>, representing the world geodetic system.
    /// </summary>
    [JsonStringEnumMemberName("世界測地系")]
    World,
    /// <summary>
    /// The value <c>日本測地系</c>, representing the Japanese geodetic system.
    /// </summary>
    [JsonStringEnumMemberName("日本測地系")]
    Japan
}
