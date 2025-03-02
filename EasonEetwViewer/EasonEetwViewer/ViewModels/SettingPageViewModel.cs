using System.Collections.ObjectModel;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using EasonEetwViewer.Dmdata.Api.Abstractions;
using EasonEetwViewer.Dmdata.Api.Dtos.Enum.WebSocket;
using EasonEetwViewer.Dmdata.Api.Dtos.Record.Contract;
using EasonEetwViewer.Dmdata.Api.Dtos.Record.WebSocket;
using EasonEetwViewer.Dmdata.Api.Dtos.Request;
using EasonEetwViewer.Dmdata.Api.Dtos.Response;
using EasonEetwViewer.Dmdata.Authentication.Abstractions;
using EasonEetwViewer.Dmdata.Authentication.Events;
using EasonEetwViewer.Dmdata.Telegram.Abstractions;
using EasonEetwViewer.Dmdata.WebSocket.Abstractions;
using EasonEetwViewer.Events;
using EasonEetwViewer.KyoshinMonitor.Abstractions;
using EasonEetwViewer.Lang;
using EasonEetwViewer.Models.SettingPage;
using EasonEetwViewer.Services.Kmoni.Abstractions;
using EasonEetwViewer.Services.TimeProvider;
using EasonEetwViewer.ViewModels.ViewModelBases;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.ViewModels;

internal sealed partial class SettingPageViewModel : PageViewModelBase
{
    public SettingPageViewModel(
        WebSocketStartPost startPost,
        IKmoniSettingsHelper kmoniOptions,
        IWebSocketClient webSocketClient,
        ILogger<SettingPageViewModel> logger,
        IAuthenticationHelper authenticator,
        IApiCaller apiCaller,
        ITelegramRetriever telegramRetriever,
        ITimeProvider timeProvider)
        : base(authenticator,
            apiCaller,
            telegramRetriever,
            timeProvider,
            logger)
    {
        _webSocketClient = webSocketClient;
        _startPost = startPost;
        _logger = logger;
        KmoniSettingsHelper = kmoniOptions;
        _authenticator.StatusChanged += AuthenticationStatusChangedEventHandler;
        _webSocketClient.StatusChanged += WebSocketStatusChangedEventHandler;
    }
    private readonly ILogger<SettingPageViewModel> _logger;

    #region languageSettings
    public event EventHandler<LanguagedChangedEventArgs>? LanguageChanged;

    [ObservableProperty]
    private CultureInfo _languageChoice = Resources.Culture;

    partial void OnLanguageChoiceChanged(CultureInfo value)
    {
        LanguageChanged?.Invoke(this, new() { Language = value });
    }

    public IEnumerable<CultureInfo> LanguageChoices { get; init; } = [
        CultureInfo.InvariantCulture,
        new CultureInfo("ja-JP"),
        new CultureInfo("zh-CN")];
    #endregion

    #region kmoniSettings
    public IKmoniSettingsHelper KmoniSettingsHelper { get; init; }
    public IEnumerable<SensorType> SensorTypeChoices { get; init; }
        = Enum.GetValues<SensorType>();
    public IEnumerable<MeasurementType> MeasurementTypeChoices { get; init; }
        = Enum.GetValues<MeasurementType>();
    #endregion

    #region webSocketSettings
    private readonly IWebSocketClient _webSocketClient;
    private readonly WebSocketStartPost _startPost;
    public string WebSocketButtonText
        => _webSocketClient.IsWebSocketConnected
            ? SettingPageResources.WebSocketButtonTextConnected
            : SettingPageResources.WebSocketButtonTextDisconnected;
    private void WebSocketStatusChangedEventHandler(object? sender, EventArgs e)
        => OnPropertyChanged(nameof(WebSocketButtonText));
    [RelayCommand]
    private async Task WebSocketButton()
    {
        if (!_webSocketClient.IsWebSocketConnected)
        {
            WebSocketStart? webSocket = await _apiCaller.PostWebSocketStartAsync(_startPost);
            if (webSocket is not null)
            {
                await _webSocketClient.ConnectAsync(webSocket.WebSockerUrl.Url);
            }
        }
        else
        {
            await _webSocketClient.DisconnectAsync();
        }
    }

    [ObservableProperty]
    private ObservableCollection<IWebSocketConnectionTemplate> _webSocketConnections
        = [];

    private async Task<int> GetAvaliableWebSocketConnections()
    {
        ContractList? contractList = await _apiCaller.GetContractListAsync();
        IEnumerable<Contract> contracts = contractList?.ItemList ?? [];
        return contracts.Sum(contract
            => contract.IsValid
                ? contract.ConnectionCounts
                : 0);
    }

    [RelayCommand]
    private async Task WebSocketRefresh()
    {
        IList<WebSocketDetails> wsList = [];
        string? currentCursorToken = null;
        do
        {
            WebSocketList? webSocketList = await _apiCaller.GetWebSocketListAsync(
                limit: 100,
                connectionStatus: ConnectionStatus.Open,
                cursorToken: currentCursorToken);
            wsList.AddRange(webSocketList?.ItemList ?? []);

            currentCursorToken = webSocketList?.NextToken;
        } while (currentCursorToken is not null);

        ObservableCollection<IWebSocketConnectionTemplate> currentConnections = [];
        currentConnections.AddRange(wsList.Select(x
            => new WebSocketConnectionTemplate()
            {
                ApplicationName = x.ApplicationName ?? string.Empty,
                WebSocketId = x.WebSocketId,
                StartTime = x.StartTime,
                DisconnectTask = async ()
                    => await _apiCaller.DeleteWebSocketAsync(x.WebSocketId)
            }));

        int avaliableConnection = await GetAvaliableWebSocketConnections();
        currentConnections.AddRange(Enumerable.Repeat(
            EmptyWebSocketConnectionTemplate.Instance,
            avaliableConnection - currentConnections.Count));

        WebSocketConnections = currentConnections;
    }
    #endregion

    #region authSettings
    /// <summary>
    /// The OAuth Button text.
    /// </summary>
    public string OAuthButtonText
        => OAuthConnected
            ? SettingPageResources.OAuthButtonTextConnected
            : SettingPageResources.OAuthButtonTextDisconnected;
    /// <summary>
    /// Whether OAuth is connected.
    /// </summary>
    public bool OAuthConnected
        => AuthenticationStatus is AuthenticationStatus.OAuth;
    /// <summary>
    /// Whether the API key is confirmed.
    /// </summary>
    public bool ApiKeyConfirmed
        => AuthenticationStatus is AuthenticationStatus.ApiKey;
    /// <summary>
    /// Whether the API key button is enabled.
    /// </summary>
    public bool ApiKeyButtonEnabled
        => AuthenticationStatus is AuthenticationStatus.Null && ApiKeyText.StartsWith("AKe.");
    /// <summary>
    /// The API Key Text input.
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ApiKeyButtonEnabled))]
    private string _apiKeyText = string.Empty;
    /// <summary>
    /// The OAuth Button click option.
    /// </summary>
    /// <returns>A <see cref="Task"/> object that represents the asynchronous operation.</returns>
    [RelayCommand]
    private async Task OAuthButton()
    {
        if (AuthenticationStatus is AuthenticationStatus.OAuth)
        {
            await _authenticator.UnsetAuthenticatorAsync();
        }
        else
        {
            await _authenticator.SetOAuthAsync();
        }
    }
    /// <summary>
    /// Handles the authentication status changed event by notifying the relavant properties.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The arguments of the event.</param>
    private void AuthenticationStatusChangedEventHandler(object? sender, AuthenticationStatusChangedEventArgs e)
    {
        OnPropertyChanged(nameof(AuthenticationStatus));
        OnPropertyChanged(nameof(OAuthConnected));
        OnPropertyChanged(nameof(OAuthButtonText));
        OnPropertyChanged(nameof(ApiKeyConfirmed));
        OnPropertyChanged(nameof(ApiKeyButtonEnabled));
    }
    /// <summary>
    /// The API Key Button click option.
    /// </summary>
    /// <returns>A <see cref="Task"/> object that represents the asynchronous operation.</returns>
    [RelayCommand]
    private async Task ApiKeyButton()
        => await _authenticator.SetApiKeyAsync(ApiKeyText);
    /// <summary>
    /// Executes when the API Key text is changed, to unset the authenticator if it was originally set to API Key.
    /// </summary>
    /// <param name="value">The new value.</param>
    async partial void OnApiKeyTextChanged(string value)
    {
        if (AuthenticationStatus is AuthenticationStatus.ApiKey)
        {
            await _authenticator.UnsetAuthenticatorAsync();
        }
    }
    /// <summary>
    /// The current authentication status.
    /// </summary>
    public AuthenticationStatus AuthenticationStatus
        => _authenticator.AuthenticationStatus;
    #endregion
}