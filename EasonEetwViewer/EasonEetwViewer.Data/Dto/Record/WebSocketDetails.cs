using System.Text.Json.Serialization;
using EasonEetwViewer.HttpRequest.Dto.Enum;

namespace EasonEetwViewer.HttpRequest.Dto.Record;

/// <summary>
/// Details of a WebSocket connection.
/// </summary>
public record WebSocketDetails
{
    /// <summary>
    /// The property <c>ticket</c>. The ticket for the WebSocket connection.
    /// <c>null</c> when status is not waiting.
    /// </summary>
    [JsonPropertyName("ticket")]
    public required string? Ticket { get; init; }
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
    /// The property <c>types</c>. The types of telegrams the WebSocket receives.
    /// <c>null</c> when receiving all types from the classifications.
    /// </summary>
    [JsonPropertyName("types")]
    public required PrintList<string>? Types { get; init; }
    /// <summary>
    /// The property <c>appName</c>. The application name of the WebSocket connection.
    /// <c>null</c> when not indicated.
    /// </summary>
    [JsonPropertyName("appName")]
    public required string? ApplicationName { get; init; }
    /// <summary>
    /// The property <c>id</c>. The ID of the connection.
    /// </summary>
    [JsonPropertyName("id")]
    public required int WebSocketId { get; init; }
    /// <summary>
    /// The property <c>start</c>. The start time of the connection.
    /// </summary>
    [JsonPropertyName("start")]
    public required DateTime StartTime { get; init; }
    /// <summary>
    /// The property <c>end</c>. The end time of the connection.
    /// <c>null</c> when the connection is still open.
    /// </summary>
    [JsonPropertyName("end")]
    public required DateTime? EndTime { get; init; }
    /// <summary>
    /// The property <c>ping</c>. The previous ping-pong time of the connection.
    /// <c>null</c> when no ping-pong has been initiated/
    /// </summary>
    [JsonPropertyName("ping")]
    public required DateTime? PingTime { get; init; }
    /// <summary>
    /// The property <c>ipAddress</c>. The IP Address of the connection.
    /// <c>null</c> when the connection has never started.
    /// </summary>
    [JsonPropertyName("ipAddress")]
    public required string? IpAddress { get; init; }
    /// <summary>
    /// The property <c>server</c>. The server that the connection connected to.
    /// </summary>
    [JsonPropertyName("server")]
    public required string ServerLocation { get; init; }
    /// <summary>
    /// The property <c>status</c>. The status of the WebSocket connection.
    /// </summary>
    [JsonPropertyName("status")]
    public required ConnectionStatus WebSocketStatus { get; init; }
}
