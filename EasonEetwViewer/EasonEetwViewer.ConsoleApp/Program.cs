using EasonEetwViewer.Dto.Http.Enum;
using EasonEetwViewer.Dto.Http.Request;
using EasonEetwViewer.Dto.Http.Request.Enum;
using EasonEetwViewer.Dto.Http.Response;
using Microsoft.Extensions.Configuration;
namespace EasonEetwViewer.ConsoleApp;

internal class Program
{
    static async Task Main()
    {
        IConfigurationRoot? config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
        string baseApi = config["BaseApi"] ?? string.Empty;
        string apiKey = config["ApiKey"] ?? string.Empty;
        Data.ApiCaller apiCaller = new(baseApi, apiKey);

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

        //await apiCaller.DeleteWebSocketAsync(int.Parse(Console.ReadLine() ?? string.Empty));
        //Console.WriteLine("Successfully closed WebSocket connection.");

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
}