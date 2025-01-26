using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dto.WebSocket;

[JsonConverter(typeof(JsonStringEnumConverter<EncodingType>))]
internal enum EncodingType
{
    Unknown = 0,
    [JsonStringEnumMemberName("base64")]
    Base64 = 1,
    [JsonStringEnumMemberName("utf8")]
    Utf8 = 2
}
