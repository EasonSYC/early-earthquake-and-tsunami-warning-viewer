using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using EasonEetwViewer.Authentication;
using EasonEetwViewer.HttpRequest;
using EasonEetwViewer.HttpRequest.Dto.Record;
using EasonEetwViewer.HttpRequest.Dto.Responses;
using EasonEetwViewer.Models;
using EasonEetwViewer.Services;
using EasonEetwViewer.Services.KmoniOptions;
using EasonEetwViewer.ViewModels.ViewModelBases;

namespace EasonEetwViewer.ViewModels;
internal partial class PageViewModelBase : ViewModelBase
{
    private protected StaticResources _resources;
    internal KmoniOptions KmoniOptions { get; private init; }
    private protected AuthenticatorDto _authenticatorDto;
    private protected ApiCaller _apiCaller;
    private protected TelegramRetriever _telegramRetriever;

    // https://stackoverflow.com/a/5822249

    internal PageViewModelBase(StaticResources resources, KmoniOptions kmoniOptions, AuthenticatorDto authenticatorDto, ApiCaller apiCaller, TelegramRetriever telegramRetriever)
    {
        _resources = resources;
        KmoniOptions = kmoniOptions;
        _authenticatorDto = authenticatorDto;
        _apiCaller = apiCaller;
        _telegramRetriever = telegramRetriever;
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

    private protected virtual void OnAuthenticatorChanged()
    {
        ;
    }

    private protected AuthenticationStatus _authenticationStatus =>
        Authenticator is EmptyAuthenticator ? AuthenticationStatus.None :
        Authenticator is ApiKey ? AuthenticationStatus.ApiKey : AuthenticationStatus.OAuth;

    private protected List<Station>? _earthquakeObservationStations = null;
    private protected bool _isStationsRetrieved = false;
    private protected async Task UpdateEarthquakeObservationStations()
    {
        EarthquakeParameterResponse rsp = await _apiCaller.GetEarthquakeParameterAsync();
        _earthquakeObservationStations = rsp.ItemList;
        _isStationsRetrieved = true;
    }
}
