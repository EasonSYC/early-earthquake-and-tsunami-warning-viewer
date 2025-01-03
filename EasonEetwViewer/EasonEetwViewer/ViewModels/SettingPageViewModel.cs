using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace EasonEetwViewer.ViewModels;
internal partial class SettingPageViewModel : ViewModelBase
{
    private const string _webSocketButtonTextDisconnected = "Connect to WebSocket";
    private const string _webSocketButtonTextConnected = "Disconnect from WebSocket";
    internal string WebSocketButtonText => WebSocketConnectionStatus ? _webSocketButtonTextConnected : _webSocketButtonTextDisconnected;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(WebSocketButtonText))]
    private bool _webSocketConnectionStatus = false;

    [RelayCommand]
    private void WebSocketButton() => WebSocketConnectionStatus ^= true;

    private readonly string _oAuthTextDisconnected = string.Empty;
    private const string _oAuthTextConnected = "Connected!";
    internal string OAuthText => OAuthStatus ? _oAuthTextConnected : _oAuthTextDisconnected;

    private const string _oAuthButtonTextDisconnected = "Connect to OAuth 2.0";
    private const string _oAuthButtonTextConnected = "Disconnect from OAuth 2.0";
    internal string OAuthButtonText => OAuthStatus ? _oAuthButtonTextConnected : _oAuthButtonTextDisconnected;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(OAuthText))]
    [NotifyPropertyChangedFor(nameof(OAuthButtonText))]
    [NotifyPropertyChangedFor(nameof(AuthenticationStatusText))]
    private bool _oAuthStatus = false;

    [RelayCommand]
    private void OAuthButton()
    {
        OAuthStatus ^= true;
        ApiKeyConfirmed = false;
        ApiKeyButtonEnabled = !OAuthStatus;
    }

    private const string _apiKeyConfirmedText = "Confirmed!";
    private readonly string _apiKeyUnconfirmedText = string.Empty;
    internal string ApiKeyConfirmationText => ApiKeyConfirmed ? _apiKeyConfirmedText : _apiKeyUnconfirmedText;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ApiKeyConfirmationText))]
    [NotifyPropertyChangedFor(nameof(AuthenticationStatusText))]
    private bool _apiKeyConfirmed = false;

    [ObservableProperty]
    private bool _apiKeyButtonEnabled = true;

    [ObservableProperty]
    private string _apiKeyText = string.Empty;

    [RelayCommand]
    private void ApiKeyButton()
    {
        ApiKeyButtonEnabled = false;
        ApiKeyConfirmed = true;
    }
    partial void OnApiKeyTextChanged(string value)
    {
        if (!OAuthStatus)
        {
            ApiKeyButtonEnabled = true;
            ApiKeyConfirmed = false;
        }
    }

    private const string _oAuthInUseText = "OAuth 2.0 In Use";
    private const string _apiKeyInUseText = "API Key In Use";
    private const string _nothingInUseText = "Please Configure Authentication Method";
    public string AuthenticationStatusText => OAuthStatus ? _oAuthInUseText : ApiKeyConfirmed ? _apiKeyInUseText : _nothingInUseText;
}
