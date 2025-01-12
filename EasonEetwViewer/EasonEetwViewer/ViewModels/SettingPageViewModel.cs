using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasonEetwViewer.HttpRequest.Dto.Enum;
using EasonEetwViewer.HttpRequest.Dto.Record;
using EasonEetwViewer.HttpRequest.Dto.Responses;
using EasonEetwViewer.KyoshinMonitor.Dto.Enum;
using EasonEetwViewer.Models;

namespace EasonEetwViewer.ViewModels;

internal partial class SettingPageViewModel(UserOptions options) : PageViewModelBase(options)
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
        ContractListResponse contractList = await Options.ApiClient.GetContractListAsync();
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
            WebSocketListResponse webSocketList = await Options.ApiClient.GetWebSocketListAsync(connectionStatus: WebSocketConnectionStatus.Open, cursorToken: currentCursorToken);
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
                currentConnections.Add(new(x.WebSocketId, x.ApplicationName ?? string.Empty, x.StartTime, Options.ApiClient.DeleteWebSocketAsync))
        );

        int avaliableConnection = await GetAvaliableWebSocketConnections();
        while (currentConnections.Count < avaliableConnection)
        {
            currentConnections.Add(WebSocketConnectionTemplate.EmptyConnection);
        }

        WebSocketConnections = currentConnections;
    }

    private protected override void OptionPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        base.OptionPropertyChanged(sender, e);

        if (e.PropertyName == nameof(Options.AuthenticationStatus))
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
        Options.AuthenticationStatus == AuthenticationStatus.OAuth
        ? _oAuthTextConnected : _oAuthTextDisconnected;

    private const string _oAuthButtonTextDisconnected = "Connect to OAuth 2.0";
    private const string _oAuthButtonTextConnected = "Disconnect from OAuth 2.0";
    internal string OAuthButtonText =>
        Options.AuthenticationStatus == AuthenticationStatus.OAuth
        ? _oAuthButtonTextConnected : _oAuthButtonTextDisconnected;

    [RelayCommand]
    private async Task OAuthButton()
    {
        if (Options.AuthenticationStatus == AuthenticationStatus.OAuth)
        {
            await Options.UnsetAuthenticatorAsync();
        }
        else
        {
            await Options.SetAuthenticatorToOAuthAsync();
        }
    }

    private const string _apiKeyConfirmedText = "Confirmed!";
    private readonly string _apiKeyUnconfirmedText = string.Empty;
    internal string ApiKeyConfirmationText =>
        Options.AuthenticationStatus == AuthenticationStatus.ApiKey
        ? _apiKeyConfirmedText : _apiKeyUnconfirmedText;

    internal bool ApiKeyButtonEnabled => Options.AuthenticationStatus == AuthenticationStatus.None;

    [ObservableProperty]
    private string _apiKeyText = string.Empty;

    [RelayCommand]
    private void ApiKeyButton() => Options.SetAuthenticatorToApiKey(ApiKeyText);
    async partial void OnApiKeyTextChanged(string value)
    {
        if (Options.AuthenticationStatus == AuthenticationStatus.ApiKey)
        {
            await Options.UnsetAuthenticatorAsync();
        }
    }

    private const string _oAuthInUseText = "OAuth 2.0 In Use";
    private const string _apiKeyInUseText = "API Key In Use";
    private const string _nothingInUseText = "Please Configure Authentication Method";
    public string AuthenticationStatusText => Options.AuthenticationStatus switch
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