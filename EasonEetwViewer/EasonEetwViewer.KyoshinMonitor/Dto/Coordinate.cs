using System.Text.Json.Serialization;

namespace EasonEetwViewer.KyoshinMonitor.Dto;
public record Coordinate
{
    [JsonPropertyName("x")]
    [JsonInclude]
    public required int X { get; init; }
    [JsonPropertyName("y")]
    [JsonInclude]
    public required int Y { get; init; }
}
