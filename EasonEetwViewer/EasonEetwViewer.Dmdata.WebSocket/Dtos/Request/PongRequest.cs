using System.Text.Json.Serialization;
namespace EasonEetwViewer.WebSocket.Dtos.Request;

/// <summary>
/// Represents a pong request from the client.
/// </summary>
internal record PongRequest
{
    /// <summary>
    /// The property <c>type</c>, a constant <see cref="MessageType.Pong"/>.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("type")]
    public MessageType Type { get; } = MessageType.Pong;
    /// <summary>
    /// The property <c>pingId</c>, the Ping ID that was indicated by the server to be included.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("pingId")]
    public required string PingId { get; init; }
}