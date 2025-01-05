﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasonEetwViewer.KyoshinMonitor.Dto.Enum;
using EasonEetwViewer.Models;

namespace EasonEetwViewer.ViewModels;

internal partial class SettingPageViewModel(ApplicationOptions options) : PageViewModelBase(options)
{
    private const string _webSocketButtonTextDisconnected = "Connect to WebSocket";
    private const string _webSocketButtonTextConnected = "Disconnect from WebSocket";
    internal string WebSocketButtonText => WebSocketConnectionStatus ? _webSocketButtonTextConnected : _webSocketButtonTextDisconnected;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(WebSocketButtonText))]
    private bool _webSocketConnectionStatus = false;

    [RelayCommand]
    private void WebSocketButton() => WebSocketConnectionStatus ^= true;

    private protected override void OptionPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        base.OptionPropertyChanged(sender, e);

        if (e.PropertyName == nameof(Options.CurrentAuthenticationStatus))
        {
            OnPropertyChanged(nameof(OAuthText));
            OnPropertyChanged(nameof(OAuthButtonText));
            OnPropertyChanged(nameof(ApiKeyConfirmationText));
            OnPropertyChanged(nameof(ApiKeyButtonEnabled));
            OnPropertyChanged(nameof(AuthenticationStatusText));
        }
    }

    private readonly string _oAuthTextDisconnected = string.Empty;
    private const string _oAuthTextConnected = "Connected!";
    internal string OAuthText =>
        Options.CurrentAuthenticationStatus == AuthenticationStatus.OAuth
        ? _oAuthTextConnected : _oAuthTextDisconnected;


    private const string _oAuthButtonTextDisconnected = "Connect to OAuth 2.0";
    private const string _oAuthButtonTextConnected = "Disconnect from OAuth 2.0";
    internal string OAuthButtonText =>
        Options.CurrentAuthenticationStatus == AuthenticationStatus.OAuth
        ? _oAuthButtonTextConnected : _oAuthButtonTextDisconnected;

    [RelayCommand]
    private void OAuthButton()
    {
        if (Options.CurrentAuthenticationStatus == AuthenticationStatus.OAuth)
        {
            Options.UnsetAuthenticator();
        }
        else
        {
            Options.SetAuthenticatorToOAuth();
        }
    }

    private const string _apiKeyConfirmedText = "Confirmed!";
    private readonly string _apiKeyUnconfirmedText = string.Empty;
    internal string ApiKeyConfirmationText =>
        Options.CurrentAuthenticationStatus == AuthenticationStatus.ApiKey
        ? _apiKeyConfirmedText : _apiKeyUnconfirmedText;

    internal bool ApiKeyButtonEnabled => Options.CurrentAuthenticationStatus == AuthenticationStatus.None;

    [ObservableProperty]
    private string _apiKeyText = string.Empty;

    [RelayCommand]
    private void ApiKeyButton() => Options.SetAuthenticatorToApiKey(ApiKeyText);
    partial void OnApiKeyTextChanged(string value)
    {
        if (Options.CurrentAuthenticationStatus == AuthenticationStatus.ApiKey)
        {
            Options.UnsetAuthenticator();
        }
    }

    private const string _oAuthInUseText = "OAuth 2.0 In Use";
    private const string _apiKeyInUseText = "API Key In Use";
    private const string _nothingInUseText = "Please Configure Authentication Method";
    public string AuthenticationStatusText => Options.CurrentAuthenticationStatus switch
    {
        AuthenticationStatus.OAuth => _oAuthInUseText,
        AuthenticationStatus.ApiKey => _apiKeyInUseText,
        AuthenticationStatus.None or _ => _nothingInUseText
    };

    internal ObservableCollection<Tuple<SensorType, string>> SensorTypeChoices { get; init; } =
        new(Enum.GetValues<SensorType>()
            .Select(e => new Tuple<SensorType, string>(e, e.ToReadableString())));
    internal ObservableCollection<Tuple<KmoniDataType, string>> DataTypeChoices { get; init; } =
        new(Enum.GetValues<KmoniDataType>()
            .Select(e => new Tuple<KmoniDataType, string>(e, e.ToReadableString())));
}