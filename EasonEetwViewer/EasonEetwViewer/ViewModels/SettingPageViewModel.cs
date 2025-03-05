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
using Microsoft.Extensions.Options;

namespace EasonEetwViewer.ViewModels;

/// <summary>
/// The view model for the settings page.
/// </summary>
internal sealed partial class SettingPageViewModel : PageViewModelBase
{
    /// <summary>
    /// Instantiates a new <see cref="SettingPageViewModel"/>.
    /// </summary>
    /// <param name="startPost">The post data when the WebSocket starts.</param>
    /// <param name="kmoniOptions">The options helper for kmoni.</param>
    /// <param name="webSocketClient">The WebSocket client to be used.</param>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="authenticator">The authenticator to be used.</param>
    /// <param name="apiCaller">The API caller to be used.</param>
    /// <param name="telegramRetriever">The telegram retriever to be used.</param>
    /// <param name="telegramParser">The telegram parser to be used.</param>
    /// <param name="timeProvider">The time provider to be used.</param>
    public SettingPageViewModel(
        IOptions<WebSocketStartPost> startPost,
        IKmoniSettingsHelper kmoniOptions,
        IWebSocketClient webSocketClient,
        IAuthenticationHelper authenticator,
        IApiCaller apiCaller,
        ITelegramRetriever telegramRetriever,
        ITelegramParser telegramParser,
        ITimeProvider timeProvider,
        ILogger<SettingPageViewModel> logger)
        : base(
            authenticator,
            apiCaller,
            telegramRetriever,
            telegramParser,
            timeProvider,
            logger)
    {
        _webSocketClient = webSocketClient;
        _startPost = startPost.Value;
        _logger = logger;
        KmoniSettingsHelper = kmoniOptions;
        _authenticator.StatusChanged += AuthenticationStatusChangedEventHandler;
        _webSocketClient.StatusChanged += WebSocketStatusChangedEventHandler;
    }
    /// <summary>
    /// The logger to be used.
    /// </summary>
    private readonly ILogger<SettingPageViewModel> _logger;
    #region languageSettings
    /// <summary>
    /// Invoked when the language choice is changed.
    /// </summary>
    public event EventHandler<LanguagedChangedEventArgs>? LanguageChanged;
    /// <summary>
    /// The current language.
    /// </summary>
    [ObservableProperty]
    private CultureInfo _languageChoice = EarthquakeResources.Culture;
    /// <summary>
    /// Executes then the language choice is changed.
    /// </summary>
    /// <param name="value">The new language.</param>
    partial void OnLanguageChoiceChanged(CultureInfo value)
        => LanguageChanged?.Invoke(this, new() { Language = value });
    /// <summary>
    /// The list of languages available.
    /// </summary>
    public IEnumerable<CultureInfo> LanguageChoices { get; init; } = [
        CultureInfo.InvariantCulture,
        new CultureInfo("ja-JP"),
        new CultureInfo("zh-CN")];
    #endregion

    #region kmoniSettings
    /// <summary>
    /// The helper for kmoni options.
    /// </summary>
    public IKmoniSettingsHelper KmoniSettingsHelper { get; init; }
    /// <summary>
    /// The choices for the sensor type.
    /// </summary>
    public IEnumerable<SensorType> SensorTypeChoices { get; init; }
        = Enum.GetValues<SensorType>();
    /// <summary>
    /// The choices for the measurement type.
    /// </summary>
    public IEnumerable<MeasurementType> MeasurementTypeChoices { get; init; }
        = Enum.GetValues<MeasurementType>();
    #endregion

    #region webSocketSettings
    /// <summary>
    /// The WebSocket client to be used.
    /// </summary>
    private readonly IWebSocketClient _webSocketClient;
    /// <summary>
    /// The data to include in the POST request when starting the WebSocket.
    /// </summary>
    private readonly WebSocketStartPost _startPost;
    /// <summary>
    /// The text on the WebSocket connect/disconnect button.
    /// </summary>
    public string WebSocketButtonText
        => _webSocketClient.IsWebSocketConnected
            ? SettingPageResources.WebSocketButtonTextConnected
            : SettingPageResources.WebSocketButtonTextDisconnected;
    /// <summary>
    /// Handles the WebSocket status changed event by notifying the relavant properties.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The arguments of the event.</param>
    private void WebSocketStatusChangedEventHandler(object? sender, EventArgs e)
        => OnPropertyChanged(nameof(WebSocketButtonText));
    /// <summary>
    /// The command to execute when the connect/disconnect button for WebSocket is pressed.
    /// </summary>
    /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
    [RelayCommand]
    private async Task WebSocketButton()
    {
        if (!_webSocketClient.IsWebSocketConnected)
        {
            _logger.ConnectingWebSocket();
            WebSocketStart? webSocket = await _apiCaller.PostWebSocketStartAsync(_startPost);
            if (webSocket is not null)
            {
                await _webSocketClient.ConnectAsync(webSocket.WebSockerUrl.Url);
                _logger.ConnectedWebSocket();
            }
        }
        else
        {
            _logger.DisconnectingWebSocket();
            await _webSocketClient.DisconnectAsync();
        }
    }
    /// <summary>
    /// The list of WebSocket connections.
    /// </summary>
    public ObservableCollection<IWebSocketConnectionTemplate> WebSocketConnections { get; init; }
        = [];
    /// <summary>
    /// Gets the number of available websocket connections.
    /// </summary>
    /// <returns>The number of connections available.</returns>
    private async Task<int> GetTotalWebSocketSlots()
    {
        _logger.RequestingAvailableConnections();
        ContractList? contractList = await _apiCaller.GetContractListAsync();
        IEnumerable<Contract> contracts = contractList?.ItemList ?? [];
        int slotNumber = contracts.Sum(contract
            => contract.IsValid
                ? contract.ConnectionCounts
                : 0);
        _logger.RequestedAvailableConnections(slotNumber);
        return slotNumber;
    }
    /// <summary>
    /// The action to be executed when refreshing the WebSocket list.
    /// </summary>
    /// <returns>A <see cref="Task"/> object representing the asynchronous operation.</returns>
    [RelayCommand]
    private async Task WebSocketRefresh()
    {
        _logger.RequestingAvailableConnections();
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

        _logger.DisplayingActiveConnections();
        WebSocketConnections.Clear();
        WebSocketConnections.AddRange(wsList.Select(x
            => new WebSocketConnectionTemplate(x, async ()
                    => await _apiCaller.DeleteWebSocketAsync(x.WebSocketId))));

        int avaliableConnection = await GetTotalWebSocketSlots();
        _logger.DisplayingAvailableConnections();
        WebSocketConnections.AddRange(Enumerable.Repeat(
            EmptyWebSocketConnectionTemplate.Instance,
            avaliableConnection - WebSocketConnections.Count));
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
            _logger.UnsettingOAuth();
            await _authenticator.UnsetAuthenticatorAsync();
        }
        else
        {
            _logger.SettingOAuth();
            await _authenticator.SetOAuthAsync();
            _logger.OAuthSet();
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
    {
        _logger.SettingApiKey();
        await _authenticator.SetApiKeyAsync(ApiKeyText);
    }
    /// <summary>
    /// Executes when the API Key text is changed, to unset the authenticator if it was originally set to API Key.
    /// </summary>
    /// <param name="value">The new value.</param>
    async partial void OnApiKeyTextChanged(string value)
    {
        _logger.TextChanged();
        if (AuthenticationStatus is AuthenticationStatus.ApiKey)
        {
            _logger.UnsettingApiKey();
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