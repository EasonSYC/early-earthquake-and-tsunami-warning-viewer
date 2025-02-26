using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dto.ApiResponse.Enum.Earthquake;

/// <summary>
/// Describes the type of earthquake.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<Type>))]
public enum Type
{
    /// <summary>
    /// The value <c>normal</c>, representing an earthquake within close vicinity of Japan.
    /// </summary>
    [JsonStringEnumMemberName("normal")]
    Japan,
    /// <summary>
    /// The value <c>distant</c>, representing a distant earthquake.
    /// </summary>
    [JsonStringEnumMemberName("distant")]
    Distant
}