using System.Text.Json.Serialization;

namespace EasonEetwViewer.WebSocket.Dtos.Data;

/// <summary>
/// Represents the type of compression used in the WebSocket.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<CompressionType>))]
internal enum CompressionType
{
    /// <summary>
    /// The value <c>gzip</c>, representing Gzip compression.
    /// </summary>
    [JsonStringEnumMemberName("gzip")]
    Gzip,
    /// <summary>
    /// The value <c>zip</c>, representing Zip compression.
    /// </summary>
    [JsonStringEnumMemberName("zip")]
    Zip
}
