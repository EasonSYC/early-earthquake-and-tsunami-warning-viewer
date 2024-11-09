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
    }
}