using EasonEetwViewer.Authentication.Abstractions;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.ViewModels.ViewModelBases;
/// <summary>
/// Logs for <see cref="PageViewModelBase"/>.
/// </summary>
internal static partial class PageViewModelBaseLogs
{
    /// <summary>
    /// Log when authenticator changed.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="authenticationStatus">The new authentication status.</param>
    [LoggerMessage(
        EventId = 0,
        EventName = nameof(AuthenticatorChanged),
        Level = LogLevel.Information,
        Message = "Authenticator changed to `{AuthenticationStatus}`.")]
    public static partial void AuthenticatorChanged(
        this ILogger<PageViewModelBase> logger, AuthenticationStatus authenticationStatus);
}
