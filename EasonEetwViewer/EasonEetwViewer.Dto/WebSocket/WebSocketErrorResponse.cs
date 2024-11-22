using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.WebSocket;

/// <summary>
/// Represents an error response from the WebSocket.
/// </summary>
public record WebSocketErrorResponse : WebSocketResponse
{
    /// <summary>
    /// The property <c>type</c>, a constant <c>WebSocketResponseType.Error</c>.
    /// </summary>
    [JsonPropertyName("type")]
    public override WebSocketResponseType Type { get; init; } = WebSocketResponseType.Error;
    /// <summary>
    /// The property <c>error</c>, the error message.
    /// </summary>
    [JsonPropertyName("error")]
    public required string Error { get; init; }
    /// <summary>
    /// The property <c>code</c>, the error code.
    /// </summary>
    [JsonPropertyName("code")]
    public required int Code { get; init; }
    /// <summary>
    /// The property <c>close</c>, whether the error was fatal and the WebSocket connectikon is closed.
    /// </summary>
    [JsonPropertyName("close")]
    public required bool Close { get; init; }
}