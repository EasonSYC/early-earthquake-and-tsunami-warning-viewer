using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using EasonEetwViewer.Dto.Http.Request;
using EasonEetwViewer.Dto.Http.Response;

namespace EasonEetwViewer.Data;

public class ApiCaller
{
    private readonly string _baseApi;
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options = new()
    {
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };

    public ApiCaller(string baseApi, string apiKey)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(baseApi, nameof(baseApi));
        ArgumentException.ThrowIfNullOrWhiteSpace(apiKey, nameof(apiKey));

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
        ContractList contractList = JsonSerializer.Deserialize<ContractList>(responseBody, _options) ?? throw new Exception();
        return contractList;
    }

    public async Task<WebSocketList> GetWebSocketListAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync(_baseApi + "/socket");
        _ = response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        WebSocketList webSocketList = JsonSerializer.Deserialize<WebSocketList>(responseBody, _options) ?? throw new Exception();
        return webSocketList;
    }

    public async Task<WebSocketStartResponse> PostWebSocketStartAsync(WebSocketStartPost postData)
    {
        string postDataJson = JsonSerializer.Serialize(postData);
        StringContent content = new(postDataJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync(_baseApi + "/socket", content);
        _ = response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        WebSocketStartResponse startResponse = JsonSerializer.Deserialize<WebSocketStartResponse>(responseBody, _options) ?? throw new Exception();
        return startResponse;
    }

    public async Task DeleteWebSocketAsync(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(id, nameof(id));
        HttpResponseMessage response = await _httpClient.DeleteAsync(_baseApi + "/socket/" + id);
        _ = response.EnsureSuccessStatusCode();
        return;
    }

    public async Task<EarthquakeParameter> GetEarthquakeParameterAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync(_baseApi + "/parameter/earthquake/station");
        _ = response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        EarthquakeParameter earthquakeParameter = JsonSerializer.Deserialize<EarthquakeParameter>(responseBody, _options) ?? throw new Exception();
        return earthquakeParameter;
    }

    public async Task<PastEarthquakeList> GetPastEarthquakeListAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync(_baseApi + "/gd/earthquake");
        _ = response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        PastEarthquakeList pastEarthquakeList = JsonSerializer.Deserialize<PastEarthquakeList>(responseBody, _options) ?? throw new Exception();
        return pastEarthquakeList;
    }
}
