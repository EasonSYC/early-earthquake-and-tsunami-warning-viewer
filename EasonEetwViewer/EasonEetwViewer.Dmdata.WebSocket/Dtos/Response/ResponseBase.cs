using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.WebSocket.Dtos.Response;

/// <summary>
/// Represents a response received from the WebSocket.
/// </summary>
internal record ResponseBase
{
    /// <summary>
    /// The property <c>type</c>, representing the type of the response the WebSocket receives.
    /// </summary>
    [JsonPropertyName("type")]
    public virtual MessageType Type { get; init; }
}