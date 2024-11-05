using System.Text.Json;

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
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(apiKey + ":");
        string val = Convert.ToBase64String(plainTextBytes);
        _httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + val);
    }

    public async Task<Dto.ContractList> GetContractListAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync(_baseApi + "/contract");
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        Dto.ContractList contractList = JsonSerializer.Deserialize<Dto.ContractList>(responseBody) ?? new();
        return contractList;
    }
}
