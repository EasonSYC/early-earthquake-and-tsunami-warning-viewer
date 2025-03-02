using System.Text.Json.Serialization;

namespace EasonEetwViewer.WebSocket.Dtos.Response;

/// <summary>
/// Represents an error response from the WebSocket.
/// </summary>
internal record ErrorResponse : ResponseBase
{
    /// <summary>
    /// The property <c>type</c>, a constant <see cref="MessageType.Error"/>.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("type")]
    public override MessageType Type { get; init; } = MessageType.Error;
    /// <summary>
    /// The property <c>error</c>, the error message.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("error")]
    public required string Error { get; init; }
    /// <summary>
    /// The property <c>code</c>, the error code.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("code")]
    public required int Code { get; init; }
    /// <summary>
    /// The property <c>close</c>, whether the error was fatal and the WebSocket connection is closed.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("close")]
    public required bool Close { get; init; }
}