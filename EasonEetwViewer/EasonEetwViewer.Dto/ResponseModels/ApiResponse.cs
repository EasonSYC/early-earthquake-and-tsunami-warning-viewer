using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.ResponseModels;

public abstract record ApiResponse
{
    [JsonPropertyName("responseId")]
    public string Id { get; init; } = string.Empty;
    [JsonPropertyName("responseTime")]
    public DateTime Time { get; init; }
    [JsonPropertyName("status")]
    public ResponseEnums.Status Status { get; init; }
}