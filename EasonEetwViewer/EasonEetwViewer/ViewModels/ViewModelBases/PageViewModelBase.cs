using EasonEetwViewer.Authentication;
using EasonEetwViewer.HttpRequest;
using EasonEetwViewer.HttpRequest.Dto.Record;
using EasonEetwViewer.HttpRequest.Dto.Responses;
using EasonEetwViewer.Models;
using EasonEetwViewer.Services;
using EasonEetwViewer.Services.KmoniOptions;
using EasonEetwViewer.ViewModels.ViewModelBases;

namespace EasonEetwViewer.ViewModels.ViewModelBases;
internal partial class PageViewModelBase : ViewModelBase
{
    private protected StaticResources _resources;
    internal KmoniOptions KmoniOptions { get; private init; }
    private protected AuthenticatorDto _authenticatorDto;
    private protected IApiCaller _apiCaller;
    private protected TelegramRetriever _telegramRetriever;

    private readonly OnAuthenticatorChanged OnChange;

    // https://stackoverflow.com/a/5822249

    internal PageViewModelBase(StaticResources resources, KmoniOptions kmoniOptions, AuthenticatorDto authenticatorDto, IApiCaller apiCaller, TelegramRetriever telegramRetriever, OnAuthenticatorChanged onChange)
    {
        _resources = resources;
        KmoniOptions = kmoniOptions;
        _authenticatorDto = authenticatorDto;
        _apiCaller = apiCaller;
        _telegramRetriever = telegramRetriever;
        OnChange = onChange;
    }

    private protected IAuthenticator Authenticator
    {
        get => _authenticatorDto.Authenticator;
        set
        {
            _authenticatorDto.Authenticator = value;
            OnAuthenticatorChanged();
        }
    }

    private protected virtual void OnAuthenticatorChanged() => OnChange(_authenticatorDto);

    private protected AuthenticationStatus AuthenticationStatus =>
        Authenticator is EmptyAuthenticator ? AuthenticationStatus.None :
        Authenticator is ApiKey ? AuthenticationStatus.ApiKey : AuthenticationStatus.OAuth;
    private protected List<Station>? _earthquakeObservationStations = null;
    private protected bool IsStationsRetrieved => _earthquakeObservationStations is not null;
    private protected async Task UpdateEarthquakeObservationStations()
    {
        EarthquakeParameterResponse rsp = await _apiCaller.GetEarthquakeParameterAsync();
        _earthquakeObservationStations = rsp.ItemList;
    }
}
