using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto;

public record ApiResponse
{
    [JsonPropertyName("responseId")]
    public string Id { get; init; } = string.Empty;
    [JsonPropertyName("responseTime")]
    public DateTime Time { get; init; } = new();
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;
}
