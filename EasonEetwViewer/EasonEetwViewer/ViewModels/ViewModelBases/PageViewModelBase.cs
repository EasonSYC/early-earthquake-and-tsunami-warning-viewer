using EasonEetwViewer.Authentication;
using EasonEetwViewer.HttpRequest;
using EasonEetwViewer.HttpRequest.Dto.Record;
using EasonEetwViewer.HttpRequest.Dto.Responses;
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

    #region earthquakeObservationStations
    private protected List<Station>? _earthquakeObservationStations = null;
    private protected bool IsStationsRetrieved => _earthquakeObservationStations is not null;
    private protected async Task UpdateEarthquakeObservationStations()
    {
        EarthquakeParameterResponse rsp = await _apiCaller.GetEarthquakeParameterAsync();
        _earthquakeObservationStations = rsp.ItemList;
    }
    #endregion
}
