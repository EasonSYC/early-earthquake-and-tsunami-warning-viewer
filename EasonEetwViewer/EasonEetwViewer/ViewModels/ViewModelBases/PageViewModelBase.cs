using EasonEetwViewer.Authentication;
using EasonEetwViewer.Dmdata.Caller.Interfaces;
using EasonEetwViewer.Models;
using EasonEetwViewer.Services;

namespace EasonEetwViewer.ViewModels.ViewModelBases;
internal partial class PageViewModelBase(AuthenticatorDto authenticatorDto, IApiCaller apiCaller, ITelegramRetriever telegramRetriever, OnAuthenticatorChanged onChange) : ViewModelBase
{
    private protected AuthenticatorDto _authenticatorDto = authenticatorDto;
    private protected IApiCaller _apiCaller = apiCaller;
    private protected ITelegramRetriever _telegramRetriever = telegramRetriever;

    private readonly OnAuthenticatorChanged OnChange = onChange;

    #region authentication
    private protected IAuthenticator Authenticator
    {
        get => _authenticatorDto.Authenticator;
        set
        {
            _authenticatorDto.Authenticator = value;
            OnAuthenticatorChanged();
        }
    }

    private protected virtual void OnAuthenticatorChanged()
    {
        OnChange(_authenticatorDto);
        OnPropertyChanged(nameof(AuthenticationStatus));
    }

    internal AuthenticationStatus AuthenticationStatus =>
        Authenticator is EmptyAuthenticator ? AuthenticationStatus.None :
        Authenticator is ApiKey ? AuthenticationStatus.ApiKey : AuthenticationStatus.OAuth;
    #endregion
}
