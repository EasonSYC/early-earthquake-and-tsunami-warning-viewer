using System.Text.Json.Serialization;

namespace EasonEetwViewer.HttpRequest.Dto.Enum;

/// <summary>
/// Describes the type of earthquake.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<EarthquakeType>))]
public enum EarthquakeType
{
    /// <summary>
    /// Unknown. Default value.
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// The value <c>normal</c>, representing an earthquake within close vicinity of Japan.
    /// </summary>
    [JsonStringEnumMemberName("normal")]
    Japan = 1,
    /// <summary>
    /// The value <c>distant</c>, representing a distant earthquake.
    /// </summary>
    [JsonStringEnumMemberName("distant")]
    Distant = 2
}