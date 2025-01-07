using CommunityToolkit.Mvvm.ComponentModel;
using EasonEetwViewer.Authentication;
using EasonEetwViewer.HttpRequest;
using EasonEetwViewer.KyoshinMonitor.Dto.Enum;

namespace EasonEetwViewer.Models;
internal partial class ApplicationOptions : ObservableObject
{
    [ObservableProperty]
    private Tuple<SensorType, string> _sensorChoice
        = new(SensorType.Surface, SensorType.Surface.ToReadableString());

    [ObservableProperty]
    private Tuple<KmoniDataType, string> _dataChoice
        = new(KmoniDataType.MeasuredIntensity, KmoniDataType.MeasuredIntensity.ToReadableString());

    internal ApiCaller ApiClient { get; init; }

    public ApplicationOptions() => ApiClient = new("https://api.dmdata.jp/v2/", Authenticator);

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentAuthenticationStatus))]
    private IAuthenticator _authenticator = new EmptyAuthenticator();
    internal AuthenticationStatus CurrentAuthenticationStatus =>
        Authenticator is EmptyAuthenticator ? AuthenticationStatus.None :
        Authenticator is ApiKey ? AuthenticationStatus.ApiKey : AuthenticationStatus.OAuth;

    internal void SetAuthenticatorToApiKey(string apiKey) => Authenticator = new ApiKey(apiKey);
    internal async Task SetAuthenticatorToOAuthAsync()
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
    internal async Task UnsetAuthenticatorAsync()
    {
        if (Authenticator is OAuth auth)
        {
            await auth.RevokeTokens();
        }

        Authenticator = new EmptyAuthenticator();
    }
}