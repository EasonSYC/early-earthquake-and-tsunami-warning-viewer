using System.Text.Json.Serialization;
using EasonEetwViewer.HttpRequest.Dto.ApiResponse.ResponseBase;
using EasonEetwViewer.HttpRequest.Dto.Enum;
using EasonEetwViewer.HttpRequest.Dto.Record;

namespace EasonEetwViewer.HttpRequest.Dto.ApiResponse.Response;

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
    public required List<ContractClassification> Classifications { get; init; }
    /// <summary>
    /// The property <c>test</c>. Whether the WebSocket receives test telegrams.
    /// </summary>
    [JsonPropertyName("test")]
    public required WebSocketTestStatus Test { get; init; }
    /// <summary>
    /// The property <c>types</c>. The types of telegrams the program receives.
    /// <c>null</c> when receiving all types from the classifications.
    /// </summary>
    [JsonPropertyName("types")]
    public required List<string>? Types { get; init; }
    /// <summary>
    /// The property <c>formats</c>. A list of formats of telegrams the WebSocket receives.
    /// </summary>
    [JsonPropertyName("formats")]
    public required List<WebSocketFormatType> Formats { get; init; }
    /// <summary>
    /// The property <c>appName</c>. The application name of the WebSocket connection.
    /// <c>null</c> when not indicated.
    /// </summary>
    [JsonPropertyName("appName")]
    public required string? ApplicationName { get; init; }
    /// <summary>
    /// The property <c>websocket</c>. Represents the connection details to the WebSocket.
    /// </summary>
    [JsonPropertyName("websocket")]
    public required WebSocketUrl WebSockerUrl { get; init; }
}
