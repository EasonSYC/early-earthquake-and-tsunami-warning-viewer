using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EasonEetwViewer.Dmdata.Dto.WebSocket;
internal record PassingDetail
{
    [JsonInclude]
    [JsonPropertyName("name")]
    internal required string Name { get; init; }

    [JsonInclude]
    [JsonPropertyName("time")]
    internal required DateTimeOffset Time { get; init; }
}
