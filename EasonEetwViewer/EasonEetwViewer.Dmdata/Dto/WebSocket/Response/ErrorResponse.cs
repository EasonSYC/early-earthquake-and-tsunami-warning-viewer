using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dto.WebSocket.Response;

/// <summary>
/// Represents an error response from the WebSocket.
/// </summary>
internal record ErrorResponse : ResponseBase
{
    /// <summary>
    /// The property <c>type</c>, a constant <c>WebSocketResponseType.Error</c>.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("type")]
    internal override ResponseType Type { get; init; } = ResponseType.Error;
    /// <summary>
    /// The property <c>error</c>, the error message.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("error")]
    internal required string Error { get; init; }
    /// <summary>
    /// The property <c>code</c>, the error code.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("code")]
    internal required int Code { get; init; }
    /// <summary>
    /// The property <c>close</c>, whether the error was fatal and the WebSocket connectikon is closed.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("close")]
    internal required bool Close { get; init; }
}