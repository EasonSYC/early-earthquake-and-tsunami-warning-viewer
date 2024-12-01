using System.Text.Json.Serialization;

namespace EasonEetwViewer.Authentication.OAuth2;

public record ErrorResponse
{
    [JsonPropertyName("error")]
    public required string Error { get; init; }
    [JsonPropertyName("description")]
    public required string Description { get; init; }
}