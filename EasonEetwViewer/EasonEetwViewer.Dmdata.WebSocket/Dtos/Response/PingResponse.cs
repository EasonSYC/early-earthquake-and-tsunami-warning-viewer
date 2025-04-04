using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.WebSocket.Dtos.Response;

/// <summary>
/// Represents a ping response from the WebSocket.
/// </summary>
internal record PingResponse : ResponseBase
{
    /// <summary>
    /// The property <c>type</c>, a constant <see cref="MessageType.Ping"/>.
    /// </summary>
    [JsonPropertyName("type")]
    public override MessageType Type { get; init; } = MessageType.Ping;
    /// <summary>
    /// The property <c>pingId</c>, the Ping ID that should be included when returning a corresponding Pong.
    /// </summary>
    [JsonPropertyName("pingId")]
    public required string PingId { get; init; }
}