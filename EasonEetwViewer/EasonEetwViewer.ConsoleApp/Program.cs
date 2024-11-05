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
        Console.WriteLine(contractList.Id);
        Console.WriteLine(contractList.Time);
        Console.WriteLine(contractList.Status);
        foreach (Dto.Contract contract in contractList.Items)
        {
            Console.WriteLine(contract.Id);
            Console.WriteLine(contract.PlanId);
            Console.WriteLine(contract.PlanName);
            Console.WriteLine(contract.Classification);
            Console.WriteLine(contract.Price.Day);
            Console.WriteLine(contract.Price.Month);
            Console.WriteLine(contract.Start);
            Console.WriteLine(contract.IsValid);
            Console.WriteLine(contract.ConnectionCounts);
        }
    }
}