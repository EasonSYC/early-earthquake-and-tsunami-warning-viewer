using System.Text.Json.Serialization;

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
