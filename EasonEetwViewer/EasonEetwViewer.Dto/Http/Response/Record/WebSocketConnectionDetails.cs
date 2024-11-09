using EasonEetwViewer.Dto.Http.Response.Enums;
using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.Http.Response.Records;

/// <summary>
/// Details of a WebSocket connection.
/// Inherits from <c>WebSocketBasics</c>.
/// </summary>
public record WebSocketConnectionDetails : WebSocketBasics
{
    /// <summary>
    /// The property <c>id</c>. The ID of the connection.
    /// </summary>
    [JsonPropertyName("id")]
    public int WebSocketId { get; init; }
    /// <summary>
    /// The property <c>start</c>. The start time of the connection.
    /// </summary>
    [JsonPropertyName("start")]
    public DateTime StartTime { get; init; }
    /// <summary>
    /// The property <c>end</c>. The end time of the connection.
    /// <c>null</c> when the connection is still open.
    /// </summary>
    [JsonPropertyName("end")]
    public DateTime? EndTime { get; init; }
    /// <summary>
    /// The property <c>ping</c>. The previous ping-pong time of the connection.
    /// <c>null</c> when no ping-pong has been initiated/
    /// </summary>
    [JsonPropertyName("ping")]
    public DateTime? PingTime { get; init; }
    /// <summary>
    /// The property <c>ipAddress</c>. The IP Address of the connection.
    /// <c>null</c> when the connection has never started.
    /// </summary>
    [JsonPropertyName("ipAddress")]
    public string? IpAddress { get; init; }
    /// <summary>
    /// The property <c>server</c>. The server that the connection connected to.
    /// </summary>
    [JsonPropertyName("server")]
    public string ServerLocation { get; init; } = string.Empty;
    /// <summary>
    /// The property <c>status</c>. The status of the WebSocket connection.
    /// </summary>
    [JsonPropertyName("status")]
    public ConnectionStatus WebSocketStatus { get; init; }
}
