using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.WebSocket.Dtos;

/// <summary>
/// Represents the type of response received by the WebSocket.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<MessageType>))]
internal enum MessageType
{
    /// <summary>
    /// The value <c>start</c>. Response after starting a WebSocket.
    /// </summary>
    [JsonStringEnumMemberName("start")]
    Start,
    /// <summary>
    /// The value <c>ping</c>. A server-side initiated ping.
    /// </summary>
    [JsonStringEnumMemberName("ping")]
    Ping,
    /// <summary>
    /// The value <c>pong</c>. Response of a client-side initiated ping.
    /// </summary>
    [JsonStringEnumMemberName("pong")]
    Pong,
    /// <summary>
    /// The value <c>error</c>. An error response from the WebSocket.
    /// </summary>
    [JsonStringEnumMemberName("error")]
    Error,
    /// <summary>
    /// The value <c>data</c>. A data returned by the WebSocket.
    /// </summary>
    [JsonStringEnumMemberName("data")]
    Data
}