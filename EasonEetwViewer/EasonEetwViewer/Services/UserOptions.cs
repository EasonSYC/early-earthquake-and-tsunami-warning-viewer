using System.Text.Json;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using EasonEetwViewer.Authentication;
using EasonEetwViewer.HttpRequest;
using EasonEetwViewer.HttpRequest.Dto.Record;
using EasonEetwViewer.HttpRequest.Dto.Responses;
using EasonEetwViewer.KyoshinMonitor.Dto.Enum;
using EasonEetwViewer.Models.EnumExtensions;
using EasonEetwViewer.ViewModels;
using Mapsui.Nts.Providers.Shapefile;
using Mapsui.Providers;
using Mapsui.Styles;
using Mapsui.Utilities;

namespace EasonEetwViewer.Models;

internal partial class UserOptions : ObservableObject
{
    private const string _filePath = "userOptions.json";
    [ObservableProperty]
    [property: JsonInclude]
    [property: JsonConverter(typeof(SensorChoiceConverter))]
    [property: JsonPropertyName("kmoniSensor")]
    private Tuple<SensorType, string> _sensorChoice
        = new(SensorType.Surface, SensorType.Surface.ToReadableString());
    partial void OnSensorChoiceChanged(Tuple<SensorType, string> value) => WriteJsonFile(_filePath);

    [ObservableProperty]
    [property: JsonInclude]
    [property: JsonConverter(typeof(KmoniDataChoiceConverter))]
    [property: JsonPropertyName("kmoniData")]
    private Tuple<KmoniDataType, string> _dataChoice
        = new(KmoniDataType.MeasuredIntensity, KmoniDataType.MeasuredIntensity.ToReadableString());
    partial void OnDataChoiceChanged(Tuple<KmoniDataType, string> value) => WriteJsonFile(_filePath);

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
            WriteJsonFile(_filePath);
        }
    }

    [JsonIgnore]
    internal AuthenticationStatus AuthenticationStatus =>
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

    private void WriteJsonFile(string filePath) => File.WriteAllText(filePath, JsonSerializer.Serialize(this));

    public static UserOptions FromJsonFile(string filePath) =>
        File.Exists(filePath)
        ? JsonSerializer.Deserialize<UserOptions>(File.ReadAllText(filePath)) ?? new()
        : new();

    [JsonIgnore]
    internal List<Station>? EarthquakeObservationStations { get; private set; } = null;
    internal bool IsStationsRetrieved => EarthquakeObservationStations is not null;
    internal async Task UpdateEarthquakeObservationStations()
    {
        EarthquakeParameterResponse rsp = await ApiClient.GetEarthquakeParameterAsync();
        EarthquakeObservationStations = rsp.ItemList;
    }

    private const string _baseGisFile = "Content/GisFiles/";

    //[JsonIgnore]
    //internal IProvider EewRegion { get; private init; } = new ShapeFile("GisFiles/20190125_AreaForecastEEW_GIS/緊急地震速報／地方予報区.shp");
    //[JsonIgnore]
    //internal IProvider EewPrefectureRegion { get; private init; } = new ShapeFile("GisFiles/20190125_AreaForecastLocalEEW_GIS/緊急地震速報／府県予報区.shp");
    //[JsonIgnore]
    //internal IProvider PastPrefecture { get; private init; } = ShapeFileToProvider("GisFiles/20190125_AreaInformationPrefectureEarthquake_GIS/地震情報／都道府県等.shp", true, true);

    [JsonIgnore]
    internal IProvider PastRegion { get; private init; } = ShapeFileToProvider(_baseGisFile + "Simp_20240520_AreaForecastLocalE_GIS/PastRegions.shp", true, true);

    // Adapted from https://mapsui.com/samples/ - Projection - Shapefile with Projection
    private static IProvider ShapeFileToProvider(string shapeFilePath, bool fileBasedIndex = false, bool readPrjFile = false)
        => new ProjectingProvider(new ShapeFile(shapeFilePath, fileBasedIndex, readPrjFile) { CRS = "EPSG:4326" }) { CRS = "EPSG:3857" };

    [JsonIgnore]
    internal Mapsui.Styles.IStyle HypocentreShapeStyle { get; private init; } = new SymbolStyle
    {
        BitmapId = typeof(PastPageViewModel).LoadSvgId("Resources.hypo.svg")
    };


    private const string _baseApi = "https://api.dmdata.jp/v2/";
    private const string _telegramApi = "https://data.api.dmdata.jp/v1/";

    [JsonIgnore]
    internal ApiCaller ApiClient { get; private init; }
    [JsonIgnore]
    internal TelegramRetriever TelegramRetriever { get; private init; }

    [JsonIgnore]
    internal List<PrefectureData> Prefectures { get; private init; } = [
        new PrefectureData() { Code = "01", Name = "北海道" },
        new PrefectureData() { Code = "02", Name = "青森県" },
        new PrefectureData() { Code = "03", Name = "岩手県" },
        new PrefectureData() { Code = "04", Name = "宮城県" },
        new PrefectureData() { Code = "05", Name = "秋田県" },
        new PrefectureData() { Code = "06", Name = "山形県" },
        new PrefectureData() { Code = "07", Name = "福島県" },
        new PrefectureData() { Code = "08", Name = "茨城県" },
        new PrefectureData() { Code = "09", Name = "栃木県" },
        new PrefectureData() { Code = "10", Name = "群馬県" },
        new PrefectureData() { Code = "11", Name = "埼玉県" },
        new PrefectureData() { Code = "12", Name = "千葉県" },
        new PrefectureData() { Code = "13", Name = "東京都" },
        new PrefectureData() { Code = "14", Name = "神奈川県" },
        new PrefectureData() { Code = "15", Name = "新潟県" },
        new PrefectureData() { Code = "16", Name = "富山県" },
        new PrefectureData() { Code = "17", Name = "石川県" },
        new PrefectureData() { Code = "18", Name = "福井県" },
        new PrefectureData() { Code = "19", Name = "山梨県" },
        new PrefectureData() { Code = "20", Name = "長野県" },
        new PrefectureData() { Code = "21", Name = "岐阜県" },
        new PrefectureData() { Code = "22", Name = "静岡県" },
        new PrefectureData() { Code = "23", Name = "愛知県" },
        new PrefectureData() { Code = "24", Name = "三重県" },
        new PrefectureData() { Code = "25", Name = "滋賀県" },
        new PrefectureData() { Code = "26", Name = "京都府" },
        new PrefectureData() { Code = "27", Name = "大阪府" },
        new PrefectureData() { Code = "28", Name = "兵庫県" },
        new PrefectureData() { Code = "29", Name = "奈良県" },
        new PrefectureData() { Code = "30", Name = "和歌山県" },
        new PrefectureData() { Code = "31", Name = "鳥取県" },
        new PrefectureData() { Code = "32", Name = "島根県" },
        new PrefectureData() { Code = "33", Name = "岡山県" },
        new PrefectureData() { Code = "34", Name = "広島県" },
        new PrefectureData() { Code = "35", Name = "山口県" },
        new PrefectureData() { Code = "36", Name = "徳島県" },
        new PrefectureData() { Code = "37", Name = "香川県" },
        new PrefectureData() { Code = "38", Name = "愛媛県" },
        new PrefectureData() { Code = "39", Name = "高知県" },
        new PrefectureData() { Code = "40", Name = "福岡県" },
        new PrefectureData() { Code = "41", Name = "佐賀県" },
        new PrefectureData() { Code = "42", Name = "長崎県" },
        new PrefectureData() { Code = "43", Name = "熊本県" },
        new PrefectureData() { Code = "44", Name = "大分県" },
        new PrefectureData() { Code = "45", Name = "宮崎県" },
        new PrefectureData() { Code = "46", Name = "鹿児島県" },
        new PrefectureData() { Code = "47", Name = "沖縄県" }
    ];

    [JsonConstructor]
    public UserOptions(Tuple<SensorType, string> sensorChoice, Tuple<KmoniDataType, string> dataChoice, AuthenticatorDto authenticatorWrapper)
    {
        SensorChoice = sensorChoice;
        DataChoice = dataChoice;
        AuthenticatorWrapper = authenticatorWrapper;
        ApiClient = new(_baseApi, AuthenticatorWrapper);
        TelegramRetriever = new(_telegramApi, AuthenticatorWrapper);
    }

    public UserOptions()
    {
        ApiClient = new(_baseApi, AuthenticatorWrapper);
        TelegramRetriever = new(_telegramApi, AuthenticatorWrapper);
    }
}