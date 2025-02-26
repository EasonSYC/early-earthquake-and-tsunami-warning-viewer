using System.Text.Json.Serialization;
using EasonEetwViewer.WebSocket.Dtos.Response;

namespace EasonEetwViewer.WebSocket.Dtos.Request;

/// <summary>
/// Represents a ping request from the client.
/// </summary>
internal record PingRequest : ResponseBase
{
    /// <summary>
    /// The property <c>type</c>, a constant <see cref="MessageType.Ping"/>.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("type")]
    public override MessageType Type { get; init; } = MessageType.Ping;
    /// <summary>
    /// The property <c>pingId</c>, the Ping ID that is to be included when the WebSocket returns the Pong.
    /// <see langword="null"/> when not specified by the user-initiated ping.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("pingId")]
    public string? PingId { get; init; }
}