using EasonEetwViewer.Api.Abstractions;
using EasonEetwViewer.Authentication.Abstractions;
using EasonEetwViewer.Services;
using EasonEetwViewer.Telegram.Abstractions;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.ViewModels.ViewModelBases;
internal partial class PageViewModelBase : ViewModelBase
{
    public PageViewModelBase(
        AuthenticationWrapper authenticatorWrapper,
        IApiCaller apiCaller,
        ITelegramRetriever telegramRetriever,
        ITimeProvider timeProvider,
        ILogger<PageViewModelBase> logger,
        EventHandler<AuthenticationStatusChangedEventArgs> eventHandler)
        : base(logger)
    {
        _authenticatorWrapper = authenticatorWrapper;
        _apiCaller = apiCaller;
        _telegramRetriever = telegramRetriever;
        _timeProvider = timeProvider;
        _logger = logger;

        authenticatorWrapper.AuthenticationStatusChanged += AuthenticatorDto_AuthenticationStatusChanged;
        AuthenticationStatusChanged += eventHandler;
    }

    private protected AuthenticationWrapper _authenticatorWrapper;
    private protected IApiCaller _apiCaller;
    private protected ITelegramRetriever _telegramRetriever;
    private protected ITimeProvider _timeProvider;
    private protected new ILogger<PageViewModelBase> _logger;
    #region authentication
    private protected virtual void AuthenticatorDto_AuthenticationStatusChanged(object? sender, EventArgs e)
    {
        AuthenticationStatusChanged.Invoke(this,
            new()
            {
                NewAuthenticatorString = _authenticatorWrapper.ToString()
            });

        OnPropertyChanged(nameof(AuthenticationStatus));
        _logger.AuthenticatorChanged(AuthenticationStatus);
    }
    public event EventHandler<AuthenticationStatusChangedEventArgs> AuthenticationStatusChanged;
    public AuthenticationStatus AuthenticationStatus
        => _authenticatorWrapper.AuthenticationStatus;
    #endregion
}
