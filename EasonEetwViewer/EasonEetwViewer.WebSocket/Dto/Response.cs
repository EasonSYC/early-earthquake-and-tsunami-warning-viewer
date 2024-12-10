using System.Text.Json.Serialization;

namespace EasonEetwViewer.WebSocket.Dto;

/// <summary>
/// Represents a response received from the WebSocket.
/// </summary>
internal record Response
{
    /// <summary>
    /// The property <c>type</c>, representing the type of the response the WebSocket receives.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("type")]
    internal virtual ResponseType Type { get; init; }
}