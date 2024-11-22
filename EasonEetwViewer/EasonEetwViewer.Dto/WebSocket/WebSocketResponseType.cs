using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.WebSocket;

/// <summary>
/// Represents the type of response received by the WebSocket.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<WebSocketResponseType>))]
public enum WebSocketResponseType
{
    /// <summary>
    /// Unknown. Default value.
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// The value <c>start</c>. Response after starting a WebSocket.
    /// </summary>
    [JsonStringEnumMemberName("start")]
    Start = 1,
    /// <summary>
    /// The value <c>ping</c>. A server-side initiated ping.
    /// </summary>
    [JsonStringEnumMemberName("ping")]
    Ping = 2,
    /// <summary>
    /// The value <c>pong</c>. Response of a client-side initiated ping.
    /// </summary>
    [JsonStringEnumMemberName("pong")]
    Pong = 3,
    /// <summary>
    /// The value <c>error</c>. An error response from the WebSocket.
    /// </summary>
    [JsonStringEnumMemberName("error")]
    Error = 4,
    /// <summary>
    /// The value <c>data</c>. A data returned by the WebSocket.
    /// </summary>
    [JsonStringEnumMemberName("data")]
    Data = 5
}