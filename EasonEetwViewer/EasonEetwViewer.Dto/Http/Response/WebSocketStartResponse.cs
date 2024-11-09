using System.Text.Json.Serialization;
using EasonEetwViewer.Dto.Http.Response.Record;

namespace EasonEetwViewer.Dto.Http.Response;

/// <summary>
/// Represents the result of a POST request to start WebSocket.
/// </summary>
public record WebSocketStartResponse : WebSocketBasics
{
    /// <summary>
    /// The property <c>websocket</c>. Representes the connection details to the WebSocket.
    /// </summary>
    [JsonPropertyName("websocket")]
    public WebSocketUrl WebSockerUrl { get; init; } = new();
}
