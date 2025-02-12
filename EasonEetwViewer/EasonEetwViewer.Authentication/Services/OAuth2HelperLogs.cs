using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Authentication.Services;
/// <summary>
/// Represents the log messages used in <see cref="OAuth2Helper"/>.
/// </summary>
internal static partial class OAuth2HelperLogs
{
    /// <summary>
    /// Log when instantiated.
    /// </summary>
    [LoggerMessage(
        EventId = 0,
        EventName = nameof(Instantiated),
        Level = LogLevel.Information,
        Message = "Instantiated.")]
    public static partial void Instantiated(
        this ILogger<OAuth2Helper> logger);

    /// <summary>
    /// Log when finding unused port.
    /// </summary>
    [LoggerMessage(
        EventId = 1,
        EventName = nameof(FindingUnusedPort),
        Level = LogLevel.Debug,
        Message = "Finding unused port.")]
    public static partial void FindingUnusedPort(
        this ILogger<OAuth2Helper> logger);

    /// <summary>
    /// Log when found unused port.
    /// </summary>
    [LoggerMessage(
        EventId = 2,
        EventName = nameof(FoundUnusedPort),
        Level = LogLevel.Information,
        Message = "Found unused port: `{Port}`.")]
    public static partial void FoundUnusedPort(
        this ILogger<OAuth2Helper> logger, int port);

    /// <summary>
    /// Log when starting a webpage on the user's browser.
    /// </summary>
    [LoggerMessage(
        EventId = 3,
        EventName = nameof(StartingBrowser),
        Level = LogLevel.Information,
        Message = "Starting browser page: `{Uri}`.")]
    public static partial void StartingBrowser(
        this ILogger<OAuth2Helper> logger, Uri uri);

    /// <summary>
    /// Log when starting a webpage on the user's browser.
    /// </summary>
    [LoggerMessage(
        EventId = 4,
        EventName = nameof(ResponseReceived),
        Level = LogLevel.Debug,
        Message = "Response received: `{Uri}`.")]
    public static partial void ResponseReceived(
        this ILogger<OAuth2Helper> logger, Uri uri);

    /// <summary>
    /// Log when starting a webpage on the user's browser.
    /// </summary>
    [LoggerMessage(
        EventId = 5,
        EventName = nameof(ErrorMessageReceived),
        Level = LogLevel.Error,
        Message = "Error Message received: `{Error}`.")]
    public static partial void ErrorMessageReceived(
        this ILogger<OAuth2Helper> logger, string error);

    /// <summary>
    /// Log when starting a webpage on the user's browser.
    /// </summary>
    [LoggerMessage(
        EventId = 6,
        EventName = nameof(StateDoesNotMatch),
        Level = LogLevel.Error,
        Message = "State received from authenticator does not match.")]
    public static partial void StateDoesNotMatch(
        this ILogger<OAuth2Helper> logger);

    /// <summary>
    /// Log when unable to parse JSON.
    /// </summary>
    [LoggerMessage(
        EventId = 7,
        EventName = nameof(IncorrectJsonFormat),
        Level = LogLevel.Error,
        Message = "Unable to parse JSON from server: `{Json}`.")]
    public static partial void IncorrectJsonFormat(
        this ILogger<OAuth2Helper> logger, string json);
}
