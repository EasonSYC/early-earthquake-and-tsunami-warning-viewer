using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.Dto.ApiResponse.Record.GdEarthquake;
public record TelegramHead
{
    [JsonPropertyName("type")]
    public required string Type { get; init; }
    [JsonPropertyName("author")]
    public required string Author { get; init; }
    [JsonPropertyName("time")]
    public required DateTimeOffset Time { get; init; }
    [JsonPropertyName("designation")]
    public required string? Designation { get; init; }
    [JsonPropertyName("test")]
    public bool Test { get; } = false;
}
