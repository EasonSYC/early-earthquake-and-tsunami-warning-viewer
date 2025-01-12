using System.Text.Json.Serialization;

namespace EasonEetwViewer.HttpRequest.Dto.Enum;

/// <summary>
/// Represents the format of a telegram.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<WebSocketFormatType>))]
public enum WebSocketFormatType
{
    /// <summary>
    /// Unknown. Default value.
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// The value <c>xml</c>, representing XML format.
    /// </summary>
    [JsonStringEnumMemberName("xml")]
    Xml = 1,
    /// <summary>
    /// The value <c>json</c>, representing JSON format.
    /// </summary>
    [JsonStringEnumMemberName("json")]
    Json = 2,
    /// <summary>
    /// The value <c>a/n</c>, representing alphanumeric format.
    /// </summary>
    [JsonStringEnumMemberName("a/n")]
    AlphaNumeric = 3,
    /// <summary>
    /// The value <c>binary</c>, representing binary format.
    /// </summary>
    [JsonStringEnumMemberName("binary")]
    Binary = 4
}