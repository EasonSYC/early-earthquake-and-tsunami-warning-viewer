using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
