using EasonEetwViewer.Authentication;
using EasonEetwViewer.Data;
using EasonEetwViewer.Dto.Http.Enum;
using EasonEetwViewer.Dto.Http.Request;
using EasonEetwViewer.Dto.Http.Request.Enum;
using EasonEetwViewer.Dto.Http.Response;
using EasonEetwViewer.WebSocket;
using Microsoft.Extensions.Configuration;
namespace EasonEetwViewer.ConsoleApp;

internal class Program
{
    private static async Task Main()
    {
        IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();

        string apiKey = config["ApiKey"] ?? string.Empty;
        IAuthenticator apiKeyAuth = new ApiKey(apiKey);

        IConfigurationSection oAuthConfig = config.GetSection("oAuth");
        string oAuthClientId = oAuthConfig["clientId"] ?? string.Empty;
        string oAuthBaseUri = oAuthConfig["baseUri"] ?? string.Empty;
        string oAuthHost = oAuthConfig["host"] ?? string.Empty;
        HashSet<string> oAuthScopes = oAuthConfig.GetSection("scopes").Get<HashSet<string>>() ?? [];
        IAuthenticator oAuth = new OAuth(oAuthClientId, oAuthBaseUri, oAuthHost, oAuthScopes);

        string baseApi = config["BaseApi"] ?? string.Empty;

        ApiCaller apiCaller = new(baseApi, apiKeyAuth);
        // ApiCaller apiCaller = new(baseApi, oAuth);

        WebSocketClient webSocketClient = new();

        // await TestAuthenticator(apiKey);
        // await TestAuthenticator(oAuth);
        // await TestApiCaller(apiCaller);
        await TestWebSocket(apiCaller, webSocketClient);
    }

    private static async Task TestAuthenticator(IAuthenticator auth)
    {
        Console.WriteLine(await auth.GetAuthenticationHeader());
        Console.WriteLine(await auth.GetAuthenticationHeader());
        Console.WriteLine(await auth.GetNewAuthenticationHeader());
        Console.WriteLine(await auth.GetAuthenticationHeader());
        Console.WriteLine(await auth.GetAuthenticationHeader());
        Console.WriteLine(await auth.GetNewAuthenticationHeader());
        Console.WriteLine(await auth.GetAuthenticationHeader());
        Console.WriteLine(await auth.GetAuthenticationHeader());
    }

    private static async Task TestApiCaller(ApiCaller apiCaller)
    {
        ContractList contractList = await apiCaller.GetContractListAsync();
        Console.WriteLine(contractList);

        WebSocketList webSocketList = await apiCaller.GetWebSocketListAsync();
        Console.WriteLine(webSocketList);

        WebSocketStartPost start = new()
        {
            Classifications = [Classification.EewForecast, Classification.TelegramEarthquake],
            Types = ["VXSE45", "VXSE51", "VXSE52", "VXSE53"],
            TestStatus = TestStatus.Include,
            AppName = "Test",
            FormatMode = FormatMode.Raw
        };
        WebSocketStartResponse response = await apiCaller.PostWebSocketStartAsync(start);
        Console.WriteLine(response);

        await apiCaller.DeleteWebSocketAsync(int.Parse(Console.ReadLine() ?? string.Empty));
        Console.WriteLine("Successfully closed WebSocket connection.");

        EarthquakeParameter earthquakeParameter = await apiCaller.GetEarthquakeParameterAsync();
        Console.WriteLine(earthquakeParameter.ResponseId);
        Console.WriteLine(earthquakeParameter.ResponseStatus);
        Console.WriteLine(earthquakeParameter.ResponseTime);
        Console.WriteLine(earthquakeParameter.Version);
        Console.WriteLine(earthquakeParameter.ChangeTime);
        Console.WriteLine(earthquakeParameter.ItemList[0]);

        PastEarthquakeList pastEarthquakeList = await apiCaller.GetPastEarthquakeListAsync();
        Console.WriteLine(pastEarthquakeList.ResponseId);
        Console.WriteLine(pastEarthquakeList.ResponseStatus);
        Console.WriteLine(pastEarthquakeList.ResponseTime);
        Console.WriteLine(pastEarthquakeList.NextToken);
        Console.WriteLine(pastEarthquakeList.NextPooling);
        Console.WriteLine(pastEarthquakeList.NextPoolingInterval);
        Console.WriteLine(pastEarthquakeList.ItemList[0]);
    }
    private static async Task TestWebSocket(ApiCaller apiCaller, WebSocketClient webSocketClient)
    {
        WebSocketStartPost start = new()
        {
            Classifications = [Classification.EewForecast, Classification.TelegramEarthquake],
            Types = ["VXSE45", "VXSE51", "VXSE52", "VXSE53"],
            TestStatus = TestStatus.Include,
            AppName = "Test",
            FormatMode = FormatMode.Raw
        };
        WebSocketStartResponse response = await apiCaller.PostWebSocketStartAsync(start);
        Console.WriteLine(response);

        await webSocketClient.ConnectWebSocketAsync(response.WebSockerUrl.Url);
    }
}