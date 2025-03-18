using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.WebSocket.Dtos.Response;

namespace EasonEetwViewer.Dmdata.WebSocket.Dtos.Request;

/// <summary>
/// Represents a pong request from the client.
/// </summary>
internal record PongRequest : ResponseBase
{
    /// <summary>
    /// The property <c>type</c>, a constant <see cref="MessageType.Pong"/>.
    /// </summary>
    [JsonPropertyName("type")]
    public override MessageType Type { get; init; } = MessageType.Pong;
    /// <summary>
    /// The property <c>pingId</c>, the Ping ID that was indicated by the server to be included.
    /// </summary>
    [JsonPropertyName("pingId")]
    public required string PingId { get; init; }
}