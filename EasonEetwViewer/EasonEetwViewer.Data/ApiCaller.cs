using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Headers;
using EasonEetwViewer.Authentication;
using EasonEetwViewer.Dto.Http.Request;
using EasonEetwViewer.Dto.Http.Response;

namespace EasonEetwViewer.Data;

public class ApiCaller
{
    private readonly HttpClient _client;
    private readonly IAuthenticator _authenticator;
    private readonly JsonSerializerOptions _options = new()
    {
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };

    public ApiCaller(string baseApi, IAuthenticator authenticator)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(baseApi, nameof(baseApi));

        _client = new()
        {
            BaseAddress = new(baseApi)
        };
        _authenticator = authenticator;
    }

    public async Task<ContractList> GetContractListAsync()
    {
        HttpRequestMessage request = new(HttpMethod.Get, "contract");
        request.Headers.Authorization = await _authenticator.GetAuthenticationHeader();
        HttpResponseMessage response = await _client.SendAsync(request);

        _ = response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        ContractList contractList = JsonSerializer.Deserialize<ContractList>(responseBody, _options) ?? throw new Exception();
        return contractList;
    }

    public async Task<WebSocketList> GetWebSocketListAsync()
    {
        HttpRequestMessage request = new(HttpMethod.Get, "socket");
        request.Headers.Authorization = await _authenticator.GetAuthenticationHeader();
        HttpResponseMessage response = await _client.SendAsync(request);

        _ = response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        WebSocketList webSocketList = JsonSerializer.Deserialize<WebSocketList>(responseBody, _options) ?? throw new Exception();
        return webSocketList;
    }

    public async Task<WebSocketStartResponse> PostWebSocketStartAsync(WebSocketStartPost postData)
    {
        string postDataJson = JsonSerializer.Serialize(postData);
        StringContent content = new(postDataJson, Encoding.UTF8, "application/json");
        HttpRequestMessage request = new(HttpMethod.Post, "socket");
        request.Headers.Authorization = await _authenticator.GetAuthenticationHeader();
        request.Content = content;
        HttpResponseMessage response = await _client.SendAsync(request);

        _ = response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        WebSocketStartResponse startResponse = JsonSerializer.Deserialize<WebSocketStartResponse>(responseBody, _options) ?? throw new Exception();
        return startResponse;
    }

    public async Task DeleteWebSocketAsync(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(id, nameof(id));

        HttpRequestMessage request = new(HttpMethod.Delete, $"socket/{id}");
        request.Headers.Authorization = await _authenticator.GetAuthenticationHeader();
        HttpResponseMessage response = await _client.SendAsync(request);
        
        _ = response.EnsureSuccessStatusCode();
        return;
    }

    public async Task<EarthquakeParameter> GetEarthquakeParameterAsync()
    {
        HttpRequestMessage request = new(HttpMethod.Get, "parameter/earthquake/station");
        request.Headers.Authorization = await _authenticator.GetAuthenticationHeader();
        HttpResponseMessage response = await _client.SendAsync(request);

        _ = response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        EarthquakeParameter earthquakeParameter = JsonSerializer.Deserialize<EarthquakeParameter>(responseBody, _options) ?? throw new Exception();
        return earthquakeParameter;
    }

    public async Task<PastEarthquakeList> GetPastEarthquakeListAsync()
    {
        HttpRequestMessage request = new(HttpMethod.Get, "gd/earthquake");
        request.Headers.Authorization = await _authenticator.GetAuthenticationHeader();
        HttpResponseMessage response = await _client.SendAsync(request);
        
        _ = response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        PastEarthquakeList pastEarthquakeList = JsonSerializer.Deserialize<PastEarthquakeList>(responseBody, _options) ?? throw new Exception();
        return pastEarthquakeList;
    }
}
