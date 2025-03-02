using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dtos.Enum.WebSocket;

/// <summary>
/// Represents the format of a telegram.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<FormatType>))]
public enum FormatType
{
    /// <summary>
    /// The value <c>xml</c>, representing XML format.
    /// </summary>
    [JsonStringEnumMemberName("xml")]
    Xml,
    /// <summary>
    /// The value <c>json</c>, representing JSON format.
    /// </summary>
    [JsonStringEnumMemberName("json")]
    Json,
    /// <summary>
    /// The value <c>a/n</c>, representing alphanumeric format.
    /// </summary>
    [JsonStringEnumMemberName("a/n")]
    AlphaNumeric,
    /// <summary>
    /// The value <c>binary</c>, representing binary format.
    /// </summary>
    [JsonStringEnumMemberName("binary")]
    Binary
}