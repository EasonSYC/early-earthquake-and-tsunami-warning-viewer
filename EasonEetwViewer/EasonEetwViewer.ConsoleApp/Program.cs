﻿using EasonEetwViewer.Data;
using EasonEetwViewer.Dto.Http.Enum;
using EasonEetwViewer.Dto.Http.Request;
using EasonEetwViewer.Dto.Http.Request.Enum;
using EasonEetwViewer.Dto.Http.Response;
using Microsoft.Extensions.Configuration;
using EasonEetwViewer.Authentication;
using System.Runtime.CompilerServices;
using System.Net.WebSockets;
namespace EasonEetwViewer.ConsoleApp;

internal class Program
{
    static async Task Main()
    {
        IConfigurationRoot? config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();

        string apiKey = config["ApiKey"] ?? string.Empty;
        IAuthenticator apiKeyAuth = new ApiKey(apiKey);

        string oAuthClientId = config["OAuthClientId"] ?? string.Empty;
        string oAuthBase = config["OAuthBase"] ?? string.Empty;
        string oAuthHost = config["OAuthHost"] ?? string.Empty;
        string oAuthScopes = config["OAuthScopes"] ?? string.Empty;
        IAuthenticator oAuth = new OAuth(oAuthClientId, oAuthBase, oAuthHost, oAuthScopes);

        string baseApi = config["BaseApi"] ?? string.Empty;

        // ApiCaller apiCaller = new(baseApi, apiKeyAuth);
        ApiCaller apiCaller = new(baseApi, oAuth);

        // WebSocketClient webSocketClient = new();

        await TestApiCaller(apiCaller);
    }

    static async Task TestAuthenticator(IAuthenticator auth)
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

    static async Task TestApiCaller(ApiCaller apiCaller)
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
    static async Task TestWebSocket(ApiCaller apiCaller, WebSocketClient webSocketClient)
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