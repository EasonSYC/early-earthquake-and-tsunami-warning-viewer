using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.ViewModels.ViewModelBases;
/// <summary>
/// Logs for <see cref="SettingPageViewModel"/>.
/// </summary>
internal static partial class SettingPageViewModelLogs
{
    /// <summary>
    /// Log when text in API Key input box is changed.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    [LoggerMessage(
        EventId = 0,
        EventName = nameof(TextChanged),
        Level = LogLevel.Information,
        Message = "The text in API Key box is changed.")]
    public static partial void TextChanged(
        this ILogger<SettingPageViewModel> logger);
    /// <summary>
    /// Log when editing API Key.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    [LoggerMessage(
        EventId = 1,
        EventName = nameof(UnsettingApiKey),
        Level = LogLevel.Debug,
        Message = "Unsetting API Key since the text in API Key box is changed.")]
    public static partial void UnsettingApiKey(
        this ILogger<SettingPageViewModel> logger);
    /// <summary>
    /// Log when setting API Key.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    [LoggerMessage(
        EventId = 2,
        EventName = nameof(SettingApiKey),
        Level = LogLevel.Information,
        Message = "Setting API Key.")]
    public static partial void SettingApiKey(
        this ILogger<SettingPageViewModel> logger);
    /// <summary>
    /// Log when unsetting OAuth.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    [LoggerMessage(
        EventId = 3,
        EventName = nameof(UnsettingOAuth),
        Level = LogLevel.Information,
        Message = "Unsetting OAuth.")]
    public static partial void UnsettingOAuth(
        this ILogger<SettingPageViewModel> logger);
    /// <summary>
    /// Log when setting OAuth.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    [LoggerMessage(
        EventId = 4,
        EventName = nameof(SettingOAuth),
        Level = LogLevel.Information,
        Message = "Setting OAuth.")]
    public static partial void SettingOAuth(
        this ILogger<SettingPageViewModel> logger);
    /// <summary>
    /// Log when setting OAuth set finished.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    [LoggerMessage(
        EventId = 5,
        EventName = nameof(OAuthSet),
        Level = LogLevel.Information,
        Message = "OAuth set finished.")]
    public static partial void OAuthSet(
        this ILogger<SettingPageViewModel> logger);
    /// <summary>
    /// Log when connecting to WebSocket.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    [LoggerMessage(
        EventId = 6,
        EventName = nameof(ConnectingWebSocket),
        Level = LogLevel.Information,
        Message = "Connecting to WebSocket.")]
    public static partial void ConnectingWebSocket(
        this ILogger<SettingPageViewModel> logger);
    /// <summary>
    /// Log when connected to WebSocket.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    [LoggerMessage(
        EventId = 7,
        EventName = nameof(ConnectedWebSocket),
        Level = LogLevel.Information,
        Message = "Connected to WebSocket.")]
    public static partial void ConnectedWebSocket(
        this ILogger<SettingPageViewModel> logger);
    /// <summary>
    /// Log when connected to WebSocket.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    [LoggerMessage(
        EventId = 8,
        EventName = nameof(DisconnectingWebSocket),
        Level = LogLevel.Information,
        Message = "Disconnecting from WebSocket.")]
    public static partial void DisconnectingWebSocket(
        this ILogger<SettingPageViewModel> logger);
    /// <summary>
    /// Log when getting number of available WebSocket connections.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    [LoggerMessage(
        EventId = 9,
        EventName = nameof(RequestingAvailableConnections),
        Level = LogLevel.Information,
        Message = "Ruquesting available connections.")]
    public static partial void RequestingAvailableConnections(
        this ILogger<SettingPageViewModel> logger);
    /// <summary>
    /// Log when successfully get the number of available WebSocket connections.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="number">The number of available connections</param>
    [LoggerMessage(
        EventId = 10,
        EventName = nameof(RequestedAvailableConnections),
        Level = LogLevel.Information,
        Message = "Available connections: `{Number}`.")]
    public static partial void RequestedAvailableConnections(
        this ILogger<SettingPageViewModel> logger, int number);
    /// <summary>
    /// Log when requesting current WebSocket connections.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    [LoggerMessage(
        EventId = 11,
        EventName = nameof(RequestingCurrentConnections),
        Level = LogLevel.Information,
        Message = "Requesting current WebSocket connections.")]
    public static partial void RequestingCurrentConnections(
        this ILogger<SettingPageViewModel> logger);
    /// <summary>
    /// Log when adding active WebSocket connections to current display.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    [LoggerMessage(
        EventId = 12,
        EventName = nameof(DisplayingActiveConnections),
        Level = LogLevel.Trace,
        Message = "Displaying active connections.")]
    public static partial void DisplayingActiveConnections(
        this ILogger<SettingPageViewModel> logger);
    /// <summary>
    /// Log when adding empty WebSocket connections to current display.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    [LoggerMessage(
        EventId = 13,
        EventName = nameof(DisplayingAvailableConnections),
        Level = LogLevel.Trace,
        Message = "Displaying available connections.")]
    public static partial void DisplayingAvailableConnections(
        this ILogger<SettingPageViewModel> logger);
}