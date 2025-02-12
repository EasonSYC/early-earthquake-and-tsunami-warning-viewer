using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Authentication.Services;
/// <summary>
/// Represents the log messages used in <see cref="OAuth2Authenticator"/>.
/// </summary>
internal static partial class OAuth2AuthenticatorLogs
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
        this ILogger<OAuth2Authenticator> logger);

    /// <summary>
    /// Log when revoking token.
    /// </summary>
    [LoggerMessage(
        EventId = 1,
        EventName = nameof(RevokingToken),
        Level = LogLevel.Debug,
        Message = "Revoking token: `{Token}`.")]
    public static partial void RevokingToken(
        this ILogger<OAuth2Authenticator> logger, string token);

    /// <summary>
    /// Log when token revoked.
    /// </summary>
    [LoggerMessage(
        EventId = 2,
        EventName = nameof(TokenRevoked),
        Level = LogLevel.Information,
        Message = "Token revoked.")]
    public static partial void TokenRevoked(
        this ILogger<OAuth2Authenticator> logger);

    /// <summary>
    /// Log when unable to parse JSON.
    /// </summary>
    [LoggerMessage(
        EventId = 3,
        EventName = nameof(IncorrectJsonFormat),
        Level = LogLevel.Error,
        Message = "Unable to parse JSON from server: `{Json}`.")]
    public static partial void IncorrectJsonFormat(
        this ILogger<OAuth2Authenticator> logger, string json);

    /// <summary>
    /// Log when failed to revoke token.
    /// </summary>
    [LoggerMessage(
        EventId = 4,
        EventName = nameof(TokenRevokeFailed),
        Level = LogLevel.Error,
        Message = "Unable to revoke token: `{ErrorMessage}`.")]
    public static partial void TokenRevokeFailed(
        this ILogger<OAuth2Authenticator> logger, string errorMessage);

    /// <summary>
    /// Log when requesting new access token.
    /// </summary>
    [LoggerMessage(
        EventId = 5,
        EventName = nameof(RequestingNewAccessToken),
        Level = LogLevel.Debug,
        Message = "Requesting new access token.")]
    public static partial void RequestingNewAccessToken(
        this ILogger<OAuth2Authenticator> logger);

    /// <summary>
    /// Log when requesting new access token.
    /// </summary>
    [LoggerMessage(
        EventId = 6,
        EventName = nameof(NewAccessTokenAcquired),
        Level = LogLevel.Information,
        Message = "New access token acquired.")]
    public static partial void NewAccessTokenAcquired(
        this ILogger<OAuth2Authenticator> logger);

    /// <summary>
    /// Log when an exception was thrown and handled, and the program continues execution.
    /// </summary>
    [LoggerMessage(
        EventId = 7,
        EventName = nameof(IgnoredException),
        Level = LogLevel.Warning,
        Message = "Exception: `{Exception}`.")]
    public static partial void IgnoredException(
        this ILogger<OAuth2Authenticator> logger, string exception);
}