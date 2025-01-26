using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dto.WebSocket.Response;

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
    internal virtual ResponseType Type { get; init; }
}