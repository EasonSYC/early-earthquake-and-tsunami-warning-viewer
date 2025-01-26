using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Dto.WebSocket.Response;

namespace EasonEetwViewer.Dmdata.Dto.WebSocket.Request;

/// <summary>
/// Represents a ping request from the client.
/// </summary>
internal record PingRequest : ResponseBase
{
    /// <summary>
    /// The property <c>type</c>, a constant <c>WebSocketResponseType.Ping</c>.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("type")]
    internal override ResponseType Type { get; init; } = ResponseType.Ping;
    /// <summary>
    /// The property <c>pingId</c>, the Ping ID that is to be included when the WebSocket returns the Pong.
    /// <c>null</c> when not specified by the user-initiated ping.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("pingId")]
    internal string? PingId { get; init; }
}