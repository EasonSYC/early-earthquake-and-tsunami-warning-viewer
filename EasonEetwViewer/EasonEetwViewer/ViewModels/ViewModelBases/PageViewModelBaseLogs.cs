using EasonEetwViewer.Models;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.ViewModels.ViewModelBases;
internal static partial class PageViewModelBaseLogs
{
    /// <summary>
    /// Log when authenticator changed.
    /// </summary>
    [LoggerMessage(
        EventId = 0,
        EventName = nameof(AuthenticatorChanged),
        Level = LogLevel.Information,
        Message = "Authenticator changed to `{AuthenticationStatus}`.")]
    public static partial void AuthenticatorChanged(
        this ILogger<PageViewModelBase> logger, AuthenticationStatus authenticationStatus);
}
