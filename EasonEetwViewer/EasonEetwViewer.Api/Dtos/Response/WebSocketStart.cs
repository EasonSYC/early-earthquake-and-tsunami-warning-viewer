using System.Text.Json.Serialization;
using EasonEetwViewer.Api.Dtos.Record.WebSocket;
using EasonEetwViewer.Api.Dtos.ResponseBase;
using EasonEetwViewer.Dtos.Enum;
using EasonEetwViewer.Dtos.Enum.WebSocket;

namespace EasonEetwViewer.Api.Dtos.Response;

/// <summary>
/// Represents the result of a POST request to start WebSocket.
/// </summary>
public record WebSocketStart : SuccessBase
{
    /// <summary>
    /// The property <c>ticket</c>. The ticket for the WebSocket connection.
    /// </summary>
    [JsonPropertyName("ticket")]
    public required string Ticket { get; init; }
    /// <summary>
    /// The property <c>classifications</c>. The classifications of telegrams that the WebSocket receives.
    /// </summary>
    [JsonPropertyName("classifications")]
    public required IEnumerable<Classification> Classifications { get; init; }
    /// <summary>
    /// The property <c>test</c>. Whether the WebSocket receives test telegrams.
    /// </summary>
    [JsonPropertyName("test")]
    public required TestStatus TestStatus { get; init; }
    /// <summary>
    /// The property <c>types</c>. The types of telegrams the program receives.
    /// <see langword="null"/> when receiving all types from the classifications.
    /// </summary>
    [JsonPropertyName("types")]
    public required IEnumerable<string>? Types { get; init; }
    /// <summary>
    /// The property <c>formats</c>. A list of formats of telegrams the WebSocket receives.
    /// </summary>
    [JsonPropertyName("formats")]
    public required IEnumerable<FormatType> Formats { get; init; }
    /// <summary>
    /// The property <c>appName</c>. The application name of the WebSocket connection.
    /// <see langword="null"/> when not indicated.
    /// </summary>
    [JsonPropertyName("appName")]
    public required string? ApplicationName { get; init; }
    /// <summary>
    /// The property <c>websocket</c>. Represents the connection details to the WebSocket.
    /// </summary>
    [JsonPropertyName("websocket")]
    public required WebSocketUrl WebSockerUrl { get; init; }
}
