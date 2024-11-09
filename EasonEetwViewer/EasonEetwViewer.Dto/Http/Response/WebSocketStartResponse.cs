using System.Text.Json.Serialization;
using EasonEetwViewer.Dto.Http.Enum;
using EasonEetwViewer.Dto.Http.Response.Enum;
using EasonEetwViewer.Dto.Http.Response.Model;
using EasonEetwViewer.Dto.Http.Response.Record;

namespace EasonEetwViewer.Dto.Http.Response;

/// <summary>
/// Represents the result of a POST request to start WebSocket.
/// </summary>
public record WebSocketStartResponse : SuccessResponse
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
    public required PrintList<Classification> Classifications { get; init; }
    /// <summary>
    /// The property <c>test</c>. Whether the WebSocket receives test telegrams.
    /// </summary>
    [JsonPropertyName("test")]
    public required TestStatus Test { get; init; }
    /// <summary>
    /// The property <c>types</c>. The types of telegrams the program receives.
    /// <c>null</c> when receiving all types from the classifications.
    /// </summary>
    [JsonPropertyName("types")]
    public PrintList<string>? Types { get; init; }
    /// <summary>
    /// The property <c>formats</c>. A list of formats of telegrams the WebSocket receives.
    /// </summary>
    [JsonPropertyName("formats")]
    public required PrintList<FormatType> Formats { get; init; }
    /// <summary>
    /// The property <c>appName</c>. The application name of the WebSocket connection.
    /// <c>null</c> when not indicated.
    /// </summary>
    [JsonPropertyName("appName")]
    public string? ApplicationName { get; init; }
    /// <summary>
    /// The property <c>websocket</c>. Represents the connection details to the WebSocket.
    /// </summary>
    [JsonPropertyName("websocket")]
    public required WebSocketUrl WebSockerUrl { get; init; }
}
