using System.Text.Json.Serialization;

namespace EasonEetwViewer.WebSocket.Dtos;
internal record PassingDetail
{
    [JsonInclude]
    [JsonPropertyName("name")]
    internal required string Name { get; init; }

    [JsonInclude]
    [JsonPropertyName("time")]
    internal required DateTimeOffset Time { get; init; }
}
