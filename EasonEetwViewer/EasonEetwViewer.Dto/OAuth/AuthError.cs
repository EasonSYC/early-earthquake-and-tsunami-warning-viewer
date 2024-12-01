using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.OAuth;

public record AuthError
{
    [JsonPropertyName("error")]
    public required string Error { get; init; }
    [JsonPropertyName("description")]
    public required string Description { get; init; }
}