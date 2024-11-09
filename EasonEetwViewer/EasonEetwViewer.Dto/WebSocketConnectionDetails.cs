using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto;

public record WebSocketConnectionDetails : WebSocketBasics
{
    [JsonPropertyName("id")]
    public int Id { get; init; }
}
