using EasonEetwViewer.Dmdata.WebSocket.Services;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.WebSocket.Services;
/// <summary>
/// Represents the log messages used in <see cref="WebSocketClient"/>.
/// </summary>
internal static partial class WebSocketClientLogs
{
    /// <summary>
    /// Log when instantiated.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    [LoggerMessage(
        EventId = 0,
        EventName = nameof(Instantiated),
        Level = LogLevel.Information,
        Message = "Instantiated.")]
    public static partial void Instantiated(
        this ILogger<WebSocketClient> logger);

    /// <summary>
    /// Log when connecting to WebSocket.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="url">The URI of the WebSocket.</param>
    [LoggerMessage(
        EventId = 1,
        EventName = nameof(Connecting),
        Level = LogLevel.Debug,
        Message = "Connecting to WebSocket: `{Url}`.")]
    public static partial void Connecting(
        this ILogger<WebSocketClient> logger, Uri url);

    /// <summary>
    /// Log when connected to WebSocket.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="url">The URI of the WebSocket.</param>
    [LoggerMessage(
        EventId = 2,
        EventName = nameof(Connected),
        Level = LogLevel.Information,
        Message = "Connected to WebSocket: `{Url}`.")]
    public static partial void Connected(
        this ILogger<WebSocketClient> logger, Uri url);

    /// <summary>
    /// Log when disconnecting from WebSocket.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    [LoggerMessage(
        EventId = 3,
        EventName = nameof(Disconnecting),
        Level = LogLevel.Debug,
        Message = "Disconnecting from WebSocket.")]
    public static partial void Disconnecting(
        this ILogger<WebSocketClient> logger);

    /// <summary>
    /// Log when disconnected from WebSocket.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    [LoggerMessage(
        EventId = 4,
        EventName = nameof(Disconnected),
        Level = LogLevel.Information,
        Message = "Disconnected from WebSocket.")]
    public static partial void Disconnected(
        this ILogger<WebSocketClient> logger);

    /// <summary>
    /// Log when sending data to WebSocket.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="data">The data to be sent.</param>
    [LoggerMessage(
        EventId = 5,
        EventName = nameof(Sending),
        Level = LogLevel.Debug,
        Message = "Sending data to WebSocket: `{Data}`.")]
    public static partial void Sending(
        this ILogger<WebSocketClient> logger, string data);

    /// <summary>
    /// Log when data is sent to WebSocket.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="data">The data to be sent.</param>
    [LoggerMessage(
        EventId = 6,
        EventName = nameof(Sent),
        Level = LogLevel.Information,
        Message = "Data sent to WebSocket: `{Data}`.")]
    public static partial void Sent(
        this ILogger<WebSocketClient> logger, string data);

    /// <summary>
    /// Log when task cancelled.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    [LoggerMessage(
        EventId = 7,
        EventName = nameof(TaskCancelled),
        Level = LogLevel.Debug,
        Message = "Task is cancelled.")]
    public static partial void TaskCancelled(
        this ILogger<WebSocketClient> logger);

    /// <summary>
    /// Log when unexpected exception caught.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="exception">The description of the exception.</param>
    [LoggerMessage(
        EventId = 8,
        EventName = nameof(UnexpectedException),
        Level = LogLevel.Error,
        Message = "Unexpected exception: `{Exception}`.")]
    public static partial void UnexpectedException(
        this ILogger<WebSocketClient> logger, string exception);

    /// <summary>
    /// Log when data with incorrect format received.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="data">The incorrect data received.</param>
    [LoggerMessage(
        EventId = 10,
        EventName = nameof(IncorrectFormat),
        Level = LogLevel.Error,
        Message = "Incorected format: `{Data}`.")]
    public static partial void IncorrectFormat(
        this ILogger<WebSocketClient> logger, string data);

    /// <summary>
    /// Log when sending a ping request.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="pingId">The ID of the ping request.</param>
    [LoggerMessage(
        EventId = 11,
        EventName = nameof(SendingPingRequest),
        Level = LogLevel.Trace,
        Message = "Sending ping request with ID: `{PingId}`.")]
    public static partial void SendingPingRequest(
        this ILogger<WebSocketClient> logger, string pingId);

    /// <summary>
    /// Log when number of unreceived ping request exceeded a maximum.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    [LoggerMessage(
        EventId = 13,
        EventName = nameof(UnreceivedPingExceeding),
        Level = LogLevel.Warning,
        Message = "Number of unreceived ping requests exceeded maximum.")]
    public static partial void UnreceivedPingExceeding(
        this ILogger<WebSocketClient> logger);

    /// <summary>
    /// Log when the data received is in a format that is unsupported.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    [LoggerMessage(
        EventId = 14,
        EventName = nameof(UnsupportedFormat),
        Level = LogLevel.Warning,
        Message = "The format received is not supported.")]
    public static partial void UnsupportedFormat(
        this ILogger<WebSocketClient> logger);

    /// <summary>
    /// Log when data is received.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="data">The data received.</param>
    [LoggerMessage(
        EventId = 15,
        EventName = nameof(DataReveiced),
        Level = LogLevel.Information,
        Message = "Data received: `{Data}`.")]
    public static partial void DataReveiced(
        this ILogger<WebSocketClient> logger, string data);

    /// <summary>
    /// Log when responding to a ping request.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="pongId">The pong ID specified.</param>
    [LoggerMessage(
        EventId = 16,
        EventName = nameof(ReturningPong),
        Level = LogLevel.Trace,
        Message = "Pong response: `{PongId}`.")]
    public static partial void ReturningPong(
        this ILogger<WebSocketClient> logger, string pongId);

    /// <summary>
    /// Log when a ping request is received.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="pingId">The ping ID received.</param>
    [LoggerMessage(
        EventId = 17,
        EventName = nameof(RemovingPing),
        Level = LogLevel.Trace,
        Message = "Ping response: `{PingId}`.")]
    public static partial void RemovingPing(
        this ILogger<WebSocketClient> logger, string pingId);

    /// <summary>
    /// Log when an error is reveived
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="error">The error message.</param>
    [LoggerMessage(
        EventId = 18,
        EventName = nameof(ErrorReceived),
        Level = LogLevel.Warning,
        Message = "Error received: `{Error}`.")]
    public static partial void ErrorReceived(
        this ILogger<WebSocketClient> logger, string error);

    /// <summary>
    /// Log when an error is received and the connection is closed.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="error">The error message.</param>
    [LoggerMessage(
        EventId = 19,
        EventName = nameof(ErrorReceivedAndClosed),
        Level = LogLevel.Error,
        Message = "Error received and disconnected: `{Error}`.")]
    public static partial void ErrorReceivedAndClosed(
        this ILogger<WebSocketClient> logger, string error);

    /// <summary>
    /// Log when handling data response.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    [LoggerMessage(
        EventId = 20,
        EventName = nameof(HandlingDataResponse),
        Level = LogLevel.Information,
        Message = "Handling data response.")]
    public static partial void HandlingDataResponse(
        this ILogger<WebSocketClient> logger);

    /// <summary>
    /// Log when string is decoded and decompressed.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="decodedString">The string decoded.</param>
    [LoggerMessage(
        EventId = 21,
        EventName = nameof(DecodedAndDecompressedString),
        Level = LogLevel.Information,
        Message = "Decoded string: `{DecodedString}`.")]
    public static partial void DecodedAndDecompressedString(
        this ILogger<WebSocketClient> logger, string decodedString);
}