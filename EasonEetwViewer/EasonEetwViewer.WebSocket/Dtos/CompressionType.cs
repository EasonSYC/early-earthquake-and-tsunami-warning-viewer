using System.Text.Json.Serialization;

namespace EasonEetwViewer.WebSocket.Dtos;

[JsonConverter(typeof(JsonStringEnumConverter<CompressionType>))]
internal enum CompressionType
{
    Unknown = 0,
    [JsonStringEnumMemberName("gzip")]
    Gzip = 1,
    [JsonStringEnumMemberName("zip")]
    Zip = 2
}
