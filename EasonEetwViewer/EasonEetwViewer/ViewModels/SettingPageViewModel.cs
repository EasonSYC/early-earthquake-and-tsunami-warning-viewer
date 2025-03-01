using System.Collections.ObjectModel;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using EasonEetwViewer.Api.Abstractions;
using EasonEetwViewer.Api.Dtos;
using EasonEetwViewer.Api.Dtos.Enum.WebSocket;
using EasonEetwViewer.Api.Dtos.Record.Contract;
using EasonEetwViewer.Api.Dtos.Record.WebSocket;
using EasonEetwViewer.Api.Dtos.Response;
using EasonEetwViewer.Authentication.Abstractions;
using EasonEetwViewer.KyoshinMonitor.Abstractions;
using EasonEetwViewer.Lang;
using EasonEetwViewer.Models;
using EasonEetwViewer.Services;
using EasonEetwViewer.Services.KmoniOptions;
using EasonEetwViewer.Telegram.Abstractions;
using EasonEetwViewer.ViewModels.ViewModelBases;
using EasonEetwViewer.WebSocket.Abstractions;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.ViewModels;

internal sealed partial class SettingPageViewModel : PageViewModelBase
{
    public SettingPageViewModel(
        KmoniOptions kmoniOptions,
        WebSocketStartPost startPost,
        IWebSocketClient webSocketClient,
        ILogger<SettingPageViewModel> logger,
        OnLanguageChanged onLangChange,
        IAuthenticationHelper authenticatorDto,
        IApiCaller apiCaller,
        ITelegramRetriever telegramRetriever,
        ITimeProvider timeProvider,
        EventHandler<AuthenticationStatusChangedEventArgs> eventHandler)
        : base(authenticatorDto,
            apiCaller,
            telegramRetriever,
            timeProvider,
            logger,
            eventHandler)
    {
        LanguageChanged = onLangChange;
        KmoniOptions = kmoniOptions;
        _webSocketClient = webSocketClient;
        _startPost = startPost;
        AuthenticationStatusChanged += SettingPageViewModel_AuthenticationStatusChanged;
        _webSocketClient.StatusChanged += WebSocketClient_DataReceived;
    }

    private void WebSocketClient_DataReceived(object? sender, EventArgs e)
        => OnPropertyChanged(nameof(WebSocketConnected));

    #region languageSettings
    private readonly OnLanguageChanged LanguageChanged;

    [ObservableProperty]
    private CultureInfo _languageChoice = Resources.Culture;

    partial void OnLanguageChoiceChanged(CultureInfo value)
    {
        LanguageChanged(value);
    }

    internal IEnumerable<CultureInfo> LanguageChoices { get; init; } = [CultureInfo.InvariantCulture, new CultureInfo("ja-JP"), new CultureInfo("zh-CN")];
    #endregion

    #region kmoniSettings
    internal KmoniOptions KmoniOptions { get; init; }
    internal IEnumerable<SensorType> SensorTypeChoices { get; init; } = Enum.GetValues<SensorType>();
    internal IEnumerable<MeasurementType> DataTypeChoices { get; init; } = Enum.GetValues<MeasurementType>();
    #endregion

    #region webSocketSettings
    private readonly IWebSocketClient _webSocketClient;
    private readonly WebSocketStartPost _startPost;

    internal bool WebSocketConnected => _webSocketClient.IsWebSocketConnected;

    [RelayCommand]
    private async Task WebSocketButton()
    {
        if (!WebSocketConnected)
        {
            WebSocketStart webSocket = await _apiCaller.PostWebSocketStartAsync(_startPost);
            await _webSocketClient.ConnectAsync(webSocket.WebSockerUrl.Url);
        }
        else
        {
            await _webSocketClient.DisconnectAsync();
        }
    }

    [ObservableProperty]
    private ObservableCollection<WebSocketConnectionTemplate> _webSocketConnections = [];

    private async Task<int> GetAvaliableWebSocketConnections()
    {
        ContractList contractList = await _apiCaller.GetContractListAsync();
        IEnumerable<Contract> contracts = contractList.ItemList;
        return contracts.Sum(c => c.IsValid ? c.ConnectionCounts : 0);
    }

    private readonly WebSocketConnectionTemplate _emptyConnection
        = new(-1, () => SettingPageResources.WebSocketEmptyConnectionName, new(), (x) => Task.CompletedTask, false);

    [RelayCommand]
    private async Task WebSocketRefresh()
    {
        IList<WebSocketDetails> wsList = [];
        string currentCursorToken = string.Empty;

        // Cursor Token
        for (int i = 0; i < 5; ++i)
        {
            WebSocketList webSocketList = await _apiCaller.GetWebSocketListAsync(limit: 100, connectionStatus: ConnectionStatus.Open, cursorToken: currentCursorToken);
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
        wsList = wsList.Where(x => x.WebSocketStatus == ConnectionStatus.Open).ToList();

        ObservableCollection<WebSocketConnectionTemplate> currentConnections = [];
        currentConnections.AddRange(wsList.Select(x
            => new WebSocketConnectionTemplate(x.WebSocketId, () => x.ApplicationName ?? string.Empty, x.StartTime, _apiCaller.DeleteWebSocketAsync)));

        int avaliableConnection = await GetAvaliableWebSocketConnections();
        while (currentConnections.Count < avaliableConnection)
        {
            currentConnections.Add(_emptyConnection);
        }

        WebSocketConnections = currentConnections;
    }
    #endregion

    #region authSettings
    public bool OAuthConnected
        => AuthenticationStatus is AuthenticationStatus.OAuth;
    public bool ApiKeyConfirmed
        => AuthenticationStatus is AuthenticationStatus.ApiKey;
    public bool ApiKeyButtonEnabled
        => AuthenticationStatus is AuthenticationStatus.Null && ApiKeyText.StartsWith("AKe.");
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ApiKeyButtonEnabled))]
    private string _apiKeyText = string.Empty;

    [RelayCommand]
    private async Task OAuthButton()
    {
        if (AuthenticationStatus is AuthenticationStatus.OAuth)
        {
            await UnsetAuthenticatorAsync();
        }
        else
        {
            await SetAuthenticatorToOAuthAsync();
        }
    }

    private void SettingPageViewModel_AuthenticationStatusChanged(object? sender, AuthenticationStatusChangedEventArgs e)
    {
        OnPropertyChanged(nameof(OAuthConnected));
        OnPropertyChanged(nameof(ApiKeyConfirmed));
        OnPropertyChanged(nameof(ApiKeyButtonEnabled));
    }
    [RelayCommand]
    private async Task ApiKeyButton()
        => await SetAuthenticatorToApiKeyAsync(ApiKeyText);
    async partial void OnApiKeyTextChanged(string value)
    {
        if (AuthenticationStatus is AuthenticationStatus.ApiKey)
        {
            await UnsetAuthenticatorAsync();
        }
    }
    private async Task SetAuthenticatorToApiKeyAsync(string apiKey)
        => await _authenticatorWrapper.SetApiKeyAsync(apiKey);
    private async Task SetAuthenticatorToOAuthAsync()
        => await _authenticatorWrapper.SetOAuthAsync();
    private async Task UnsetAuthenticatorAsync()
        => await _authenticatorWrapper.UnsetAuthenticatorAsync();
    #endregion
}