using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dto.WebSocket;

[JsonConverter(typeof(JsonStringEnumConverter<CompressionType>))]
internal enum CompressionType
{
    Unknown = 0,
    [JsonStringEnumMemberName("gzip")]
    Gzip = 1,
    [JsonStringEnumMemberName("zip")]
    Zip = 2
}
