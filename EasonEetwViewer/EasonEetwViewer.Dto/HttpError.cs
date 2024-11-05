using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto;

public record HttpError
{
    [JsonPropertyName("message")]
    public string Message { get; init; } = string.Empty;
    [JsonPropertyName("code")]
    public int Code { get; init; }
}