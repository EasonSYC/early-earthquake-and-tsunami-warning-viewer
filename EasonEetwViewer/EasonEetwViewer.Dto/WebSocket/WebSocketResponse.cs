using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.WebSocket;

/// <summary>
/// Represents a response received from the WebSocket.
/// </summary>
public record WebSocketResponse
{
    /// <summary>
    /// The property <c>type</c>, representing the type of the response the WebSocket receives.
    /// </summary>
    [JsonPropertyName("type")]
    public virtual WebSocketResponseType Type { get; init; }
}