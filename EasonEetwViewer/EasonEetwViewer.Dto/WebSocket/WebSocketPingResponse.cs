using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.WebSocket;

/// <summary>
/// Represents a ping response from the WebSocket.
/// </summary>
public record WebSocketPingResponse : WebSocketResponse
{
    /// <summary>
    /// The property <c>type</c>, a constant <c>WebSocketResponseType.Ping</c>.
    /// </summary>
    [JsonPropertyName("type")]
    public override WebSocketResponseType Type { get; init; } = WebSocketResponseType.Ping;
    /// <summary>
    /// The property <c>pingId</c>, the Ping ID that should be included when returning a corresponding Pong.
    /// </summary>
    [JsonPropertyName("pingId")]
    public required string PingId { get; init; }
}