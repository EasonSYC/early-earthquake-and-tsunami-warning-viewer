using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.WebSocket;

/// <summary>
/// Represents a pong response from the WebSocket.
/// </summary>
public record WebSocketPongResponse : WebSocketResponse
{
    /// <summary>
    /// The property <c>type</c>, a constant <c>WebSocketResponseType.Pong</c>.
    /// </summary>
    [JsonPropertyName("type")]
    public override WebSocketResponseType Type { get; init; } = WebSocketResponseType.Pong;
    /// <summary>
    /// The property <c>pingId</c>, the Ping ID that was included when the user initiated the Ping.
    /// <c>null</c> when not specified by the user-initiated ping.
    /// </summary>
    [JsonPropertyName("pingId")]
    public string? PingId { get; init; }
}