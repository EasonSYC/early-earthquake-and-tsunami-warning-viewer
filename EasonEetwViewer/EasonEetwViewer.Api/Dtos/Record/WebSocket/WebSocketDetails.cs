using System.Text.Json.Serialization;
using EasonEetwViewer.Api.Dtos.Enum.WebSocket;
using EasonEetwViewer.Dtos.Enum;
using EasonEetwViewer.Dtos.Enum.WebSocket;

namespace EasonEetwViewer.Api.Dtos.Record.WebSocket;

/// <summary>
/// Details of a WebSocket connection.
/// </summary>
public record WebSocketDetails
{
    /// <summary>
    /// The property <c>ticket</c>. The ticket for the WebSocket connection.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when status is not waiting.
    /// </remarks>
    [JsonPropertyName("ticket")]
    public required string? Ticket { get; init; }
    /// <summary>
    /// The property <c>classifications</c>. The classifications of telegrams that the WebSocket receives.
    /// </summary>
    [JsonPropertyName("classifications")]
    public required IEnumerable<Classification> Classifications { get; init; }
    /// <summary>
    /// The property <c>test</c>. Whether the WebSocket receives test telegrams.
    /// </summary>
    [JsonPropertyName("test")]
    public required TestStatus Test { get; init; }
    /// <summary>
    /// The property <c>types</c>. The types of telegrams the WebSocket receives.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when receiving all types from the classifications.
    /// </remarks>
    [JsonPropertyName("types")]
    public required IEnumerable<string>? Types { get; init; }
    /// <summary>
    /// The property <c>appName</c>. The application name of the WebSocket connection.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when not indicated.
    /// </remarks>
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
    public required DateTimeOffset StartTime { get; init; }
    /// <summary>
    /// The property <c>end</c>. The end time of the connection.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when the connection is still open.
    /// </remarks>
    [JsonPropertyName("end")]
    public required DateTimeOffset? EndTime { get; init; }
    /// <summary>
    /// The property <c>ping</c>. The previous ping-pong time of the connection.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when no ping-pong has been initiated.
    /// </remarks>
    [JsonPropertyName("ping")]
    public required DateTimeOffset? PingTime { get; init; }
    /// <summary>
    /// The property <c>ipAddress</c>. The IP Address of the connection.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when the connection has never started.
    /// </remarks>
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
