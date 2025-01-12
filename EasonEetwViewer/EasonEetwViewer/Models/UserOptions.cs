using System.Text.Json;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using EasonEetwViewer.Authentication;
using EasonEetwViewer.HttpRequest;
using EasonEetwViewer.KyoshinMonitor.Dto.Enum;

namespace EasonEetwViewer.Models;

internal partial class UserOptions : ObservableObject
{
    [ObservableProperty]
    [property: JsonInclude]
    [property: JsonConverter(typeof(SensorChoiceConverter))]
    [property: JsonPropertyName("kmoniSensor")]
    private Tuple<SensorType, string> _sensorChoice
        = new(SensorType.Surface, SensorType.Surface.ToReadableString());
    partial void OnSensorChoiceChanged(Tuple<SensorType, string> value) => WriteJsonFile("userOptions.json");

    [ObservableProperty]
    [property: JsonInclude]
    [property: JsonConverter(typeof(KmoniDataChoiceConverter))]
    [property: JsonPropertyName("kmoniData")]
    private Tuple<KmoniDataType, string> _dataChoice
        = new(KmoniDataType.MeasuredIntensity, KmoniDataType.MeasuredIntensity.ToReadableString());
    partial void OnDataChoiceChanged(Tuple<KmoniDataType, string> value) => WriteJsonFile("userOptions.json");

    [JsonInclude]
    [JsonPropertyName("authenticator")]
    private AuthenticatorDto AuthenticatorWrapper { get; init; } = new();

    [JsonIgnore]
    private IAuthenticator Authenticator
    {
        get => AuthenticatorWrapper.Authenticator;
        set
        {
            OnPropertyChanging(nameof(AuthenticationStatus));
            AuthenticatorWrapper.Authenticator = value;
            OnPropertyChanged(nameof(AuthenticationStatus));
            WriteJsonFile("userOptions.json");
        }
    }

    [JsonIgnore]
    internal AuthenticationStatus AuthenticationStatus =>
        Authenticator is EmptyAuthenticator ? AuthenticationStatus.None :
        Authenticator is ApiKey ? AuthenticationStatus.ApiKey : AuthenticationStatus.OAuth;

    [JsonIgnore]
    internal ApiCaller ApiClient { get; private init; }

    [JsonConstructor]
    public UserOptions(Tuple<SensorType, string> sensorChoice, Tuple<KmoniDataType, string> dataChoice, AuthenticatorDto authenticatorWrapper)
    {
        _sensorChoice = sensorChoice;
        _dataChoice = dataChoice;
        AuthenticatorWrapper = authenticatorWrapper;
        ApiClient = new("https://api.dmdata.jp/v2/", AuthenticatorWrapper);
    }

    public UserOptions() => ApiClient = new("https://api.dmdata.jp/v2/", AuthenticatorWrapper);

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

    private void WriteJsonFile(string filePath) => File.WriteAllText(filePath, JsonSerializer.Serialize(this));

    public static UserOptions FromJsonFile(string filePath) =>
        File.Exists(filePath)
        ? JsonSerializer.Deserialize<UserOptions>(File.ReadAllText(filePath)) ?? new()
        : new();
}