using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasonEetwViewer.Authentication;
using EasonEetwViewer.HttpRequest;
using EasonEetwViewer.HttpRequest.Dto.Enum;
using EasonEetwViewer.HttpRequest.Dto.Record;
using EasonEetwViewer.HttpRequest.Dto.Responses;
using EasonEetwViewer.KyoshinMonitor.Dto.Enum;
using EasonEetwViewer.Models;
using EasonEetwViewer.Models.EnumExtensions;
using EasonEetwViewer.Services;
using EasonEetwViewer.Services.KmoniOptions;

namespace EasonEetwViewer.ViewModels;

internal partial class SettingPageViewModel(StaticResources resources, KmoniOptions kmoniOptions, AuthenticatorDto authenticatorDto, ApiCaller apiCaller, TelegramRetriever telegramRetriever)
    : PageViewModelBase(resources, kmoniOptions, authenticatorDto, apiCaller, telegramRetriever)
{
    private const string _webSocketButtonTextDisconnected = "Connect to WebSocket";
    private const string _webSocketButtonTextConnected = "Disconnect from WebSocket";
    internal string WebSocketButtonText => WebSocketConnected ? _webSocketButtonTextConnected : _webSocketButtonTextDisconnected;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(WebSocketButtonText))]
    private bool _webSocketConnected = false;

    [RelayCommand]
    private void WebSocketButton() => WebSocketConnected ^= true;

    [ObservableProperty]
    private ObservableCollection<WebSocketConnectionTemplate> _webSocketConnections = [];

    private async Task<int> GetAvaliableWebSocketConnections()
    {
        ContractListResponse contractList = await _apiCaller.GetContractListAsync();
        List<Contract> contracts = contractList.ItemList;
        int result = 0;
        foreach (Contract contract in contracts)
        {
            if (contract.IsValid)
            {
                result += contract.ConnectionCounts;
            }
        }

        return result;
    }

    [RelayCommand]
    private async Task WebSocketRefresh()
    {
        List<WebSocketDetails> wsList = [];
        string currentCursorToken = string.Empty;

        // Cursor Token
        for (int i = 0; i < 5; ++i)
        {
            WebSocketListResponse webSocketList = await _apiCaller.GetWebSocketListAsync(limit: 100, connectionStatus: WebSocketConnectionStatus.Open, cursorToken: currentCursorToken);
            wsList.AddRange(webSocketList.ItemList);

            if (webSocketList.NextToken is null)
            {
                break;
            }
            else
            {
                currentCursorToken = webSocketList.NextToken;
            }
        }

        // This filtering is due to undefined filtering behaviour in the API, just in case.
        wsList = wsList.Where(x => x.WebSocketStatus == WebSocketConnectionStatus.Open).ToList();

        ObservableCollection<WebSocketConnectionTemplate> currentConnections = [];
        wsList.ForEach(
            x =>
                currentConnections.Add(new(x.WebSocketId, x.ApplicationName ?? string.Empty, x.StartTime, _apiCaller.DeleteWebSocketAsync))
        );

        int avaliableConnection = await GetAvaliableWebSocketConnections();
        while (currentConnections.Count < avaliableConnection)
        {
            currentConnections.Add(WebSocketConnectionTemplate.EmptyConnection);
        }

        WebSocketConnections = currentConnections;
    }

    private protected override void OnAuthenticatorChanged()
    {
        base.OnAuthenticatorChanged();
        OnPropertyChanged(nameof(OAuthText));
        OnPropertyChanged(nameof(OAuthButtonText));
        OnPropertyChanged(nameof(ApiKeyConfirmationText));
        OnPropertyChanged(nameof(ApiKeyButtonEnabled));
        OnPropertyChanged(nameof(AuthenticationStatusText));
    }

    private readonly string _oAuthTextDisconnected = string.Empty;
    private const string _oAuthTextConnected = "Connected!";
    internal string OAuthText =>
        _authenticationStatus == AuthenticationStatus.OAuth
        ? _oAuthTextConnected : _oAuthTextDisconnected;

    private const string _oAuthButtonTextDisconnected = "Connect to OAuth 2.0";
    private const string _oAuthButtonTextConnected = "Disconnect from OAuth 2.0";
    internal string OAuthButtonText =>
        _authenticationStatus == AuthenticationStatus.OAuth
        ? _oAuthButtonTextConnected : _oAuthButtonTextDisconnected;

    [RelayCommand]
    private async Task OAuthButton()
    {
        if (_authenticationStatus == AuthenticationStatus.OAuth)
        {
            await UnsetAuthenticatorAsync();
        }
        else
        {
            await SetAuthenticatorToOAuthAsync();
        }
    }

    private const string _apiKeyConfirmedText = "Confirmed!";
    private readonly string _apiKeyUnconfirmedText = string.Empty;
    internal string ApiKeyConfirmationText =>
        _authenticationStatus == AuthenticationStatus.ApiKey
        ? _apiKeyConfirmedText : _apiKeyUnconfirmedText;

    internal bool ApiKeyButtonEnabled => _authenticationStatus == AuthenticationStatus.None;

    [ObservableProperty]
    private string _apiKeyText = string.Empty;

    [RelayCommand]
    private void ApiKeyButton() => SetAuthenticatorToApiKey(ApiKeyText);
    async partial void OnApiKeyTextChanged(string value)
    {
        if (_authenticationStatus == AuthenticationStatus.ApiKey)
        {
            await UnsetAuthenticatorAsync();
        }
    }

    private const string _oAuthInUseText = "OAuth 2.0 In Use";
    private const string _apiKeyInUseText = "API Key In Use";
    private const string _nothingInUseText = "Please Configure Authentication Method";
    public string AuthenticationStatusText => _authenticationStatus switch
    {
        AuthenticationStatus.OAuth => _oAuthInUseText,
        AuthenticationStatus.ApiKey => _apiKeyInUseText,
        AuthenticationStatus.None or _ => _nothingInUseText
    };

    internal List<Tuple<SensorType, string>> SensorTypeChoices { get; init; } =
        new(Enum.GetValues<SensorType>()
            .Select(e => new Tuple<SensorType, string>(e, e.ToReadableString())));
    internal List<Tuple<KmoniDataType, string>> DataTypeChoices { get; init; } =
        new(Enum.GetValues<KmoniDataType>()
            .Select(e => new Tuple<KmoniDataType, string>(e, e.ToReadableString())));

    private void SetAuthenticatorToApiKey(string apiKey) => Authenticator = new ApiKey(apiKey);
    private async Task SetAuthenticatorToOAuthAsync()
    {
        Authenticator = new OAuth("CId.RRV95iuUV9FrYzeIN_BYM9Z35MJwwQen5DIwJ8JQXaTm",
            "https://manager.dmdata.jp/account/oauth2/v1/",
            "manager.dmdata.jp",
            [
            "contract.list",
            "eew.get.forecast",
            "gd.earthquake",
            "parameter.earthquake",
            "socket.close",
            "socket.list",
            "socket.start",
            "telegram.data",
            "telegram.get.earthquake",
            "telegram.list"
        ]);
        await ((OAuth)Authenticator).CheckAccessTokenAsync();
    }
    private async Task UnsetAuthenticatorAsync()
    {
        if (Authenticator is OAuth auth)
        {
            await auth.RevokeTokens();
        }

        Authenticator = new EmptyAuthenticator();
    }
}