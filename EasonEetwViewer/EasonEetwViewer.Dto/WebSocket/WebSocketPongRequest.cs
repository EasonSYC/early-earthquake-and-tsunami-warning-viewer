using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.WebSocket;

/// <summary>
/// Represents a pong request from the client.
/// </summary>
public record WebSocketPongRequest
{
    /// <summary>
    /// The property <c>type</c>, a constant <c>WebSocketResponseType.Pong</c>.
    /// </summary>
    [JsonPropertyName("type")]
    public WebSocketResponseType Type { get; } = WebSocketResponseType.Pong;
    /// <summary>
    /// The property <c>pingId</c>, the Ping ID that was indicated by the server to be included.
    /// </summary>
    [JsonPropertyName("pingId")]
    public required string PingId { get; init; }
}