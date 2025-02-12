using EasonEetwViewer.Authentication.Abstractions;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Authentication.Services;
/// <summary>
/// Represents the log messages used in <see cref="AuthenticationWrapper"/>.
/// </summary>
internal static partial class AuthenticationWrapperLogs
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
        this ILogger<AuthenticationWrapper> logger);

    /// <summary>
    /// Log when changed to API Key.
    /// </summary>
    [LoggerMessage(
        EventId = 1,
        EventName = nameof(ChangedToApiKey),
        Level = LogLevel.Information,
        Message = "Authenticator changed to API Key.")]
    public static partial void ChangedToApiKey(
        this ILogger<AuthenticationWrapper> logger);

    /// <summary>
    /// Log when unsetting authenticator.
    /// </summary>
    [LoggerMessage(
        EventId = 2,
        EventName = nameof(Unsetting),
        Level = LogLevel.Debug,
        Message = "Unsetting authenticator.")]
    public static partial void Unsetting(
        this ILogger<AuthenticationWrapper> logger);

    /// <summary>
    /// Log when authenticator unset.
    /// </summary>
    [LoggerMessage(
        EventId = 3,
        EventName = nameof(Unset),
        Level = LogLevel.Information,
        Message = "Authenticator unset.")]
    public static partial void Unset(
        this ILogger<AuthenticationWrapper> logger);

    /// <summary>
    /// Log when OAuth exception ignored.
    /// </summary>
    [LoggerMessage(
        EventId = 4,
        EventName = nameof(OAuthExceptionIgnored),
        Level = LogLevel.Warning,
        Message = "OAuth Exception: `{Exception}`.")]
    public static partial void OAuthExceptionIgnored(
        this ILogger<AuthenticationWrapper> logger, string exception);

    /// <summary>
    /// Log when other exceptions ignored.
    /// </summary>
    [LoggerMessage(
        EventId = 5,
        EventName = nameof(OtherExceptionIgnored),
        Level = LogLevel.Error,
        Message = "Exception: `{Exception}`.")]
    public static partial void OtherExceptionIgnored(
        this ILogger<AuthenticationWrapper> logger, string exception);

    /// <summary>
    /// Log when revoking OAuth 2 tokens.
    /// </summary>
    [LoggerMessage(
        EventId = 6,
        EventName = nameof(RevokingOAuth2Token),
        Level = LogLevel.Information,
        Message = "Revoking OAuth 2 tokens.")]
    public static partial void RevokingOAuth2Token(
        this ILogger<AuthenticationWrapper> logger);
}
