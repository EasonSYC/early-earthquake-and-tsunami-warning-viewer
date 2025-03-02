using System.Text.Json.Serialization;

namespace EasonEetwViewer.WebSocket.Dtos.Response;

/// <summary>
/// Represents a response received from the WebSocket.
/// </summary>
internal record ResponseBase
{
    /// <summary>
    /// The property <c>type</c>, representing the type of the response the WebSocket receives.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("type")]
    public virtual MessageType Type { get; init; }
}