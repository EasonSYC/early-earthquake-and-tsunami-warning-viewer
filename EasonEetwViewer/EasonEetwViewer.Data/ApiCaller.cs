using System.Collections.Specialized;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using EasonEetwViewer.Authentication;
using EasonEetwViewer.HttpRequest.Dto;
using EasonEetwViewer.HttpRequest.Dto.ApiPost;
using EasonEetwViewer.HttpRequest.Dto.Enum;
using EasonEetwViewer.HttpRequest.Dto.JsonTelegram;

namespace EasonEetwViewer.HttpRequest;

public class ApiCaller
{
    private readonly HttpClient _client;
    private readonly AuthenticatorDto _authenticatorDto;
    private readonly JsonSerializerOptions _options = new()
    {
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };

    private IAuthenticator Authenticator => _authenticatorDto.Authenticator;

    public ApiCaller(string baseApi, AuthenticatorDto authenticator)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(baseApi, nameof(baseApi));

        _client = new()
        {
            BaseAddress = new(baseApi)
        };
        _authenticatorDto = authenticator;
    }

    public async Task<ContractListResponse> GetContractListAsync()
    {
        using HttpRequestMessage request = new(HttpMethod.Get, "contract");
        request.Headers.Authorization = await Authenticator.GetAuthenticationHeader();
        using HttpResponseMessage response = await _client.SendAsync(request);

        _ = response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        ContractListResponse contractList = JsonSerializer.Deserialize<ContractListResponse>(responseBody, _options) ?? throw new Exception();
        return contractList;
    }

    public async Task<WebSocketListResponse> GetWebSocketListAsync(int id = -1, WebSocketConnectionStatus connectionStatus = WebSocketConnectionStatus.Unknown, string cursorToken = "", int limit = -1)
    {
        NameValueCollection queryString = [];
        if (id != -1)
        {
            queryString.Add("id", id.ToString());
        }

        if (connectionStatus != WebSocketConnectionStatus.Unknown)
        {
            queryString.Add("status", connectionStatus.ToUriString());
        }

        if (cursorToken != string.Empty)
        {
            queryString.Add("cursorToken", cursorToken);
        }

        if (limit != -1)
        {
            queryString.Add("limit", limit.ToString());
        }

        using HttpRequestMessage request = new(HttpMethod.Get, $"socket?{queryString}");
        request.Headers.Authorization = await Authenticator.GetAuthenticationHeader();
        using HttpResponseMessage response = await _client.SendAsync(request);

        _ = response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        WebSocketListResponse webSocketList = JsonSerializer.Deserialize<WebSocketListResponse>(responseBody, _options) ?? throw new Exception();
        return webSocketList;
    }

    public async Task<WebSocketStartResponse> PostWebSocketStartAsync(WebSocketStartPost postData)
    {
        string postDataJson = JsonSerializer.Serialize(postData);
        StringContent content = new(postDataJson, Encoding.UTF8, "application/json");
        using HttpRequestMessage request = new(HttpMethod.Post, "socket");
        request.Headers.Authorization = await Authenticator.GetAuthenticationHeader();
        request.Content = content;
        using HttpResponseMessage response = await _client.SendAsync(request);

        _ = response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        WebSocketStartResponse startResponse = JsonSerializer.Deserialize<WebSocketStartResponse>(responseBody, _options) ?? throw new Exception();
        return startResponse;
    }

    public async Task DeleteWebSocketAsync(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(id, nameof(id));

        using HttpRequestMessage request = new(HttpMethod.Delete, $"socket/{id}");
        request.Headers.Authorization = await Authenticator.GetAuthenticationHeader();
        using HttpResponseMessage response = await _client.SendAsync(request);

        _ = response.EnsureSuccessStatusCode();
        return;
    }

    public async Task<EarthquakeParameterResponse> GetEarthquakeParameterAsync()
    {
        using HttpRequestMessage request = new(HttpMethod.Get, "parameter/earthquake/station");
        request.Headers.Authorization = await Authenticator.GetAuthenticationHeader();
        using HttpResponseMessage response = await _client.SendAsync(request);

        _ = response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        EarthquakeParameterResponse earthquakeParameter = JsonSerializer.Deserialize<EarthquakeParameterResponse>(responseBody, _options) ?? throw new Exception();
        return earthquakeParameter;
    }

    public async Task<PastEarthquakeListResponse> GetPastEarthquakeListAsync()
    {
        using HttpRequestMessage request = new(HttpMethod.Get, "gd/earthquake");
        request.Headers.Authorization = await Authenticator.GetAuthenticationHeader();
        using HttpResponseMessage response = await _client.SendAsync(request);

        _ = response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        PastEarthquakeListResponse pastEarthquakeList = JsonSerializer.Deserialize<PastEarthquakeListResponse>(responseBody, _options) ?? throw new Exception();
        return pastEarthquakeList;
    }
}
