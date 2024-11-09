using Microsoft.Extensions.Configuration;
namespace EasonEetwViewer.ConsoleApp;

internal class Program
{
    static async Task Main()
    {
        var config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
        string baseApi = config["BaseApi"] ?? string.Empty;
        string apiKey = config["ApiKey"] ?? string.Empty;
        Data.ApiCaller apiCaller = new(baseApi, apiKey);
        Dto.ContractList contractList = await apiCaller.GetContractListAsync();
        Console.WriteLine(contractList.ResponseId);
        Console.WriteLine(contractList.ResponseTime);
        Console.WriteLine(contractList.ResponseStatus);
        contractList.ItemList.ForEach(Console.WriteLine);
    }
}