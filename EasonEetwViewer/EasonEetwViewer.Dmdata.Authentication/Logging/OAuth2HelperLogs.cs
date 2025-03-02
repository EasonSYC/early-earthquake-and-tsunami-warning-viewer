using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Dmdata.Authentication.Services;
/// <summary>
/// Represents the log messages used in <see cref="OAuth2Helper"/>.
/// </summary>
internal static partial class OAuth2HelperLogs
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
        this ILogger<OAuth2Helper> logger);

    /// <summary>
    /// Log when finding unused port.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
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
    /// <param name="logger">The logger to be used.</param>
    /// <param name="port">The port that was found.</param>
    /// 
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
    /// <param name="logger">The logger to be used.</param>
    /// <param name="uri">The URI of the page that was started.</param>
    [LoggerMessage(
        EventId = 3,
        EventName = nameof(StartingBrowser),
        Level = LogLevel.Information,
        Message = "Starting browser page: `{Uri}`.")]
    public static partial void StartingBrowser(
        this ILogger<OAuth2Helper> logger, Uri uri);

    /// <summary>
    /// Log when response is received from the user's browser.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="uri">The URI of the page where response was received.</param>
    [LoggerMessage(
        EventId = 4,
        EventName = nameof(ResponseReceived),
        Level = LogLevel.Debug,
        Message = "Response received: `{Uri}`.")]
    public static partial void ResponseReceived(
        this ILogger<OAuth2Helper> logger, Uri uri);

    /// <summary>
    /// Log when an error message was received.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="error">The error message received.</param>
    [LoggerMessage(
        EventId = 5,
        EventName = nameof(ErrorMessageReceived),
        Level = LogLevel.Error,
        Message = "Error Message received: `{Error}`.")]
    public static partial void ErrorMessageReceived(
        this ILogger<OAuth2Helper> logger, string error);

    /// <summary>
    /// Log when the states do not match.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
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
    /// <param name="logger">The logger to be used.</param>
    /// <param name="json">The JSON string that was unable to parse.</param>
    [LoggerMessage(
        EventId = 7,
        EventName = nameof(IncorrectJsonFormat),
        Level = LogLevel.Error,
        Message = "Unable to parse JSON from server: `{Json}`.")]
    public static partial void IncorrectJsonFormat(
        this ILogger<OAuth2Helper> logger, string json);
}
