using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using EasonEetwViewer.Authentication;
using EasonEetwViewer.Dto.Http.Request;
using EasonEetwViewer.Dto.Http.Response;

namespace EasonEetwViewer.Data;

public class ApiCaller
{
    private readonly string _baseApi;
    private readonly HttpClient _client = new();
    private readonly IAuthenticator _authenticator;
    private readonly JsonSerializerOptions _options = new()
    {
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };

    public ApiCaller(string baseApi, IAuthenticator authenticator)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(baseApi, nameof(baseApi));

        _baseApi = baseApi;
        _authenticator = authenticator;
    }

    public async Task<ContractList> GetContractListAsync()
    {
        _client.DefaultRequestHeaders.Clear();
        _client.DefaultRequestHeaders.Add("Authorization", await _authenticator.GetAuthenticationHeader());
        HttpResponseMessage response = await _client.GetAsync(_baseApi + "/contract");
        _ = response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        ContractList contractList = JsonSerializer.Deserialize<ContractList>(responseBody, _options) ?? throw new Exception();
        return contractList;
    }

    public async Task<WebSocketList> GetWebSocketListAsync()
    {
        _client.DefaultRequestHeaders.Clear();
        _client.DefaultRequestHeaders.Add("Authorization", await _authenticator.GetAuthenticationHeader());
        HttpResponseMessage response = await _client.GetAsync(_baseApi + "/socket");
        _ = response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        WebSocketList webSocketList = JsonSerializer.Deserialize<WebSocketList>(responseBody, _options) ?? throw new Exception();
        return webSocketList;
    }

    public async Task<WebSocketStartResponse> PostWebSocketStartAsync(WebSocketStartPost postData)
    {
        _client.DefaultRequestHeaders.Clear();
        _client.DefaultRequestHeaders.Add("Authorization", await _authenticator.GetAuthenticationHeader());
        string postDataJson = JsonSerializer.Serialize(postData);
        StringContent content = new(postDataJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync(_baseApi + "/socket", content);
        _ = response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        WebSocketStartResponse startResponse = JsonSerializer.Deserialize<WebSocketStartResponse>(responseBody, _options) ?? throw new Exception();
        return startResponse;
    }

    public async Task DeleteWebSocketAsync(int id)
    {
        _client.DefaultRequestHeaders.Clear();
        _client.DefaultRequestHeaders.Add("Authorization", await _authenticator.GetAuthenticationHeader());
        ArgumentOutOfRangeException.ThrowIfNegative(id, nameof(id));
        HttpResponseMessage response = await _client.DeleteAsync(_baseApi + "/socket/" + id);
        _ = response.EnsureSuccessStatusCode();
        return;
    }

    public async Task<EarthquakeParameter> GetEarthquakeParameterAsync()
    {
        _client.DefaultRequestHeaders.Clear();
        _client.DefaultRequestHeaders.Add("Authorization", await _authenticator.GetAuthenticationHeader());
        HttpResponseMessage response = await _client.GetAsync(_baseApi + "/parameter/earthquake/station");
        _ = response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        EarthquakeParameter earthquakeParameter = JsonSerializer.Deserialize<EarthquakeParameter>(responseBody, _options) ?? throw new Exception();
        return earthquakeParameter;
    }

    public async Task<PastEarthquakeList> GetPastEarthquakeListAsync()
    {
        _client.DefaultRequestHeaders.Clear();
        _client.DefaultRequestHeaders.Add("Authorization", await _authenticator.GetAuthenticationHeader());
        HttpResponseMessage response = await _client.GetAsync(_baseApi + "/gd/earthquake");
        _ = response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        PastEarthquakeList pastEarthquakeList = JsonSerializer.Deserialize<PastEarthquakeList>(responseBody, _options) ?? throw new Exception();
        return pastEarthquakeList;
    }
}
