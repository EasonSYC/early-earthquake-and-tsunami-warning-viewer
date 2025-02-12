using System.Text.Json.Serialization;
namespace EasonEetwViewer.WebSocket.Dtos.Request;

/// <summary>
/// Represents a pong request from the client.
/// </summary>
internal record PongRequest
{
    /// <summary>
    /// The property <c>type</c>, a constant <c>WebSocketResponseType.Pong</c>.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("type")]
    internal ResponseType Type { get; } = ResponseType.Pong;
    /// <summary>
    /// The property <c>pingId</c>, the Ping ID that was indicated by the server to be included.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("pingId")]
    internal required string PingId { get; init; }
}