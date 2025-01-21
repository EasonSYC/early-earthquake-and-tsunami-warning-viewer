using System.Collections.ObjectModel;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasonEetwViewer.Authentication;
using EasonEetwViewer.HttpRequest;
using EasonEetwViewer.HttpRequest.Dto.ApiResponse.Response;
using EasonEetwViewer.HttpRequest.Dto.Enum;
using EasonEetwViewer.HttpRequest.Dto.Record;
using EasonEetwViewer.KyoshinMonitor.Dto.Enum;
using EasonEetwViewer.Lang;
using EasonEetwViewer.Models;
using EasonEetwViewer.Services;
using EasonEetwViewer.Services.KmoniOptions;
using EasonEetwViewer.ViewModels.ViewModelBases;

namespace EasonEetwViewer.ViewModels;

internal partial class SettingPageViewModel(KmoniOptions kmoniOptions, AuthenticatorDto authenticatorDto, IApiCaller apiCaller, ITelegramRetriever telegramRetriever, OnAuthenticatorChanged onChange, OnLanguageChanged onLangChange)
    : PageViewModelBase(authenticatorDto, apiCaller, telegramRetriever, onChange)
{
    #region languageSettings
    private readonly OnLanguageChanged LanguageChanged = onLangChange;

    [ObservableProperty]
    private CultureInfo _languageChoice = Resources.Culture;

    partial void OnLanguageChoiceChanged(CultureInfo value)
    {
        LanguageChanged(value);
    }

    internal IEnumerable<CultureInfo> LanguageChoices { get; init; } = [CultureInfo.InvariantCulture, new CultureInfo("ja-JP"), new CultureInfo("zh-CN")];
    #endregion

    #region kmoniSettings
    internal KmoniOptions KmoniOptions { get; init; } = kmoniOptions;
    internal IEnumerable<SensorType> SensorTypeChoices { get; init; } = Enum.GetValues<SensorType>();
    internal IEnumerable<KmoniDataType> DataTypeChoices { get; init; } = Enum.GetValues<KmoniDataType>();
    #endregion

    #region webSocketSettings
    [ObservableProperty]
    private bool _webSocketConnected = false;

    [RelayCommand]
    private void WebSocketButton() => WebSocketConnected ^= true;

    [ObservableProperty]
    private ObservableCollection<WebSocketConnectionTemplate> _webSocketConnections = [];

    private async Task<int> GetAvaliableWebSocketConnections()
    {
        ContractList contractList = await _apiCaller.GetContractListAsync();
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

    private readonly WebSocketConnectionTemplate _emptyConnection
        = new(-1, SettingPageResources.WebSocketEmptyConnectionName, new(), (x) => Task.CompletedTask, false);

    [RelayCommand]
    private async Task WebSocketRefresh()
    {
        List<WebSocketDetails> wsList = [];
        string currentCursorToken = string.Empty;

        // Cursor Token
        for (int i = 0; i < 5; ++i)
        {
            WebSocketList webSocketList = await _apiCaller.GetWebSocketListAsync(limit: 100, connectionStatus: WebSocketConnectionStatus.Open, cursorToken: currentCursorToken);
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
            currentConnections.Add(_emptyConnection);
        }

        WebSocketConnections = currentConnections;
    }
    #endregion

    #region authSettings
    private protected override void OnAuthenticatorChanged()
    {
        base.OnAuthenticatorChanged();
        OnPropertyChanged(nameof(OAuthConnected));
        OnPropertyChanged(nameof(ApiKeyConfirmed));
        OnPropertyChanged(nameof(ApiKeyButtonEnabled));
    }

    internal bool OAuthConnected => AuthenticationStatus == AuthenticationStatus.OAuth;
    internal bool ApiKeyConfirmed => AuthenticationStatus == AuthenticationStatus.ApiKey;
    internal bool ApiKeyButtonEnabled => AuthenticationStatus == AuthenticationStatus.None;

    [RelayCommand]
    private async Task OAuthButton()
    {
        if (AuthenticationStatus == AuthenticationStatus.OAuth)
        {
            await UnsetAuthenticatorAsync();
        }
        else
        {
            await SetAuthenticatorToOAuthAsync();
        }
    }

    [ObservableProperty]
    private string _apiKeyText = string.Empty;

    [RelayCommand]
    private void ApiKeyButton() => SetAuthenticatorToApiKey(ApiKeyText);
    async partial void OnApiKeyTextChanged(string value)
    {
        if (AuthenticationStatus == AuthenticationStatus.ApiKey)
        {
            await UnsetAuthenticatorAsync();
        }
    }

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
    #endregion
}