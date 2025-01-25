using System.Text.Json.Serialization;
using EasonEetwViewer.WebSocket.Dto;

namespace EasonEetwViewer.Dmdata.Dto.WebSocket.Response;

/// <summary>
/// Represents a pong response from the WebSocket.
/// </summary>
internal record PongResponse : ResponseBase
{
    /// <summary>
    /// The property <c>type</c>, a constant <c>WebSocketResponseType.Pong</c>.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("type")]
    internal override ResponseType Type { get; init; } = ResponseType.Pong;
    /// <summary>
    /// The property <c>pingId</c>, the Ping ID that was included when the user initiated the Ping.
    /// <c>null</c> when not specified by the user-initiated ping.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("pingId")]
    internal string? PingId { get; init; }
}