using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
