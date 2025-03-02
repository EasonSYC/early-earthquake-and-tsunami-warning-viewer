using EasonEetwViewer.Api.Abstractions;
using EasonEetwViewer.Authentication.Abstractions;
using EasonEetwViewer.Services.TimeProvider;
using EasonEetwViewer.Telegram.Abstractions;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.ViewModels.ViewModelBases;
/// <summary>
/// The base view model for all pages.
/// </summary>
internal abstract partial class PageViewModelBase : ViewModelBase
{
    /// <summary>
    /// Creates a new instance of the <see cref="PageViewModelBase"/> class.
    /// </summary>
    /// <param name="authenticatorWrapper">The authenticator to be used.</param>
    /// <param name="apiCaller">The API caller to be used.</param>
    /// <param name="telegramRetriever">The telegram retriever to be used.</param>
    /// <param name="timeProvider">The time provider to be used.</param>
    /// <param name="logger">The logger to be used.</param>
    public PageViewModelBase(
        IAuthenticationHelper authenticatorWrapper,
        IApiCaller apiCaller,
        ITelegramRetriever telegramRetriever,
        ITimeProvider timeProvider,
        ILogger<PageViewModelBase> logger)
        : base(logger)
    {
        _authenticator = authenticatorWrapper;
        _apiCaller = apiCaller;
        _telegramRetriever = telegramRetriever;
        _timeProvider = timeProvider;
        _logger = logger;

        authenticatorWrapper.AuthenticationStatusChanged += AuthenticationStatusChangedEventHAndler;
    }
    /// <summary>
    /// The authenticator to be used.
    /// </summary>
    protected readonly IAuthenticationHelper _authenticator;
    /// <summary>
    /// The API caller to be used.
    /// </summary>
    protected readonly IApiCaller _apiCaller;
    /// <summary>
    /// The telegram retriever to be used.
    /// </summary>
    protected readonly ITelegramRetriever _telegramRetriever;
    /// <summary>
    /// The time provider to be used.
    /// </summary>
    protected readonly ITimeProvider _timeProvider;
    /// <summary>
    /// The logger to be used.
    /// </summary>
    private readonly ILogger<PageViewModelBase> _logger;

    /// <summary>
    /// Handles the authentication status changed event.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The event arguments.</param>
    private void AuthenticationStatusChangedEventHAndler(object? sender, EventArgs e)
    {
        OnPropertyChanged(nameof(AuthenticationStatus));
        _logger.AuthenticatorChanged(AuthenticationStatus);
    }
    /// <summary>
    /// The current authentication status.
    /// </summary>
    public AuthenticationStatus AuthenticationStatus
        => _authenticator.AuthenticationStatus;

}
