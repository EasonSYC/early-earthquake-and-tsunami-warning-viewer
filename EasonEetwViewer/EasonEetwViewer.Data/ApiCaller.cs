using System.Text.Json;
using EasonEetwViewer.Dto.Http.Response;

namespace EasonEetwViewer.Data;

public class ApiCaller
{
    private readonly string _baseApi;
    private readonly HttpClient _httpClient;
    public ApiCaller(string baseApi, string apiKey)
    {
        if (string.IsNullOrEmpty(baseApi))
        {
            throw new ArgumentNullException(nameof(baseApi));
        }

        if (string.IsNullOrEmpty(apiKey))
        {
            throw new ArgumentNullException(nameof(apiKey));
        }

        _baseApi = baseApi;
        _httpClient = new HttpClient();
        byte[]? plainTextBytes = System.Text.Encoding.UTF8.GetBytes(apiKey + ":");
        string val = Convert.ToBase64String(plainTextBytes);
        _httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + val);
    }

    public async Task<ContractList> GetContractListAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync(_baseApi + "/contract");
        _ = response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        ContractList contractList = JsonSerializer.Deserialize<ContractList>(responseBody) ?? new();
        return contractList;
    }

    public async Task<WebSocketList> GetWebSocketListAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync(_baseApi + "/socket");
        _ = response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseBody);
        WebSocketList webSocketList = JsonSerializer.Deserialize<WebSocketList>(responseBody) ?? new();
        return webSocketList;
    }
}
