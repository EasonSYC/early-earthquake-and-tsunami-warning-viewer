using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.WebSocket.Dtos.Response;

/// <summary>
/// Represents a pong response from the WebSocket.
/// </summary>
internal record PongResponse : ResponseBase
{
    /// <summary>
    /// The property <c>type</c>, a constant <see cref="MessageType.Pong"/>.
    /// </summary>
    [JsonPropertyName("type")]
    public override MessageType Type { get; init; } = MessageType.Pong;
    /// <summary>
    /// The property <c>pingId</c>, the Ping ID that was included when the user initiated the Ping.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when not specified by the user-initiated ping.
    /// </remarks>
    [JsonPropertyName("pingId")]
    public string? PingId { get; init; }
}