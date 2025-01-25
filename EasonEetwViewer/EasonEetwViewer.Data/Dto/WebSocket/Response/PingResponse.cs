using System.Text.Json.Serialization;
using EasonEetwViewer.WebSocket.Dto;

namespace EasonEetwViewer.Dmdata.Dto.WebSocket.Response;

/// <summary>
/// Represents a ping response from the WebSocket.
/// </summary>
internal record PingResponse : ResponseBase
{
    /// <summary>
    /// The property <c>type</c>, a constant <c>WebSocketResponseType.Ping</c>.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("type")]
    internal override ResponseType Type { get; init; } = ResponseType.Ping;
    /// <summary>
    /// The property <c>pingId</c>, the Ping ID that should be included when returning a corresponding Pong.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("pingId")]
    internal required string PingId { get; init; }
}