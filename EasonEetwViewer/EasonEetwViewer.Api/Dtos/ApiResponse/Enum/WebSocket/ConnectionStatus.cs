using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.Enum.WebSocket;

/// <summary>
/// Represents the status of the WebSocket connection.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<ConnectionStatus>))]
public enum ConnectionStatus
{
    /// <summary>
    /// The value <c>waiting</c>, representing awaiting connection.
    /// </summary>
    [JsonStringEnumMemberName("waiting")]
    Waiting,
    /// <summary>
    /// The value <c>open</c>, representing an active connection.
    /// </summary>
    [JsonStringEnumMemberName("open")]
    Open,
    /// <summary>
    /// The value <c>closed</c>, representing a closed connection.
    /// </summary>
    [JsonStringEnumMemberName("closed")]
    Closed
}