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
    /// Log when sending data to WebSocket;
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
    /// Log when data is sent to WebSocket;
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
}
