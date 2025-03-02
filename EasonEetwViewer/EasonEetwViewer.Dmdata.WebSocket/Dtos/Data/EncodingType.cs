using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.WebSocket.Dtos.Data;

/// <summary>
/// Represents the type of encoding used in the response.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<EncodingType>))]
internal enum EncodingType
{
    /// <summary>
    /// The value <c>base64</c>, representing Base64 encoding.
    /// </summary>
    [JsonStringEnumMemberName("base64")]
    Base64,
    /// <summary>
    /// The value <c>utf8</c>, representing UTF-8 encoding.
    /// </summary>
    [JsonStringEnumMemberName("utf8")]
    Utf8
}
