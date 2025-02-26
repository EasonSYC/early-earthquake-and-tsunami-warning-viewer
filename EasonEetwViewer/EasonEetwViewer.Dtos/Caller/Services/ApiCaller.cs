using System.Collections.Specialized;
using System.Text;
using System.Text.Json;
using System.Web;
using EasonEetwViewer.Authentication.Abstractions;
using EasonEetwViewer.Dmdata.Caller.Extensions;
using EasonEetwViewer.Dmdata.Caller.Interfaces;
using EasonEetwViewer.Dmdata.Dto.ApiPost;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Enum;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Enum.WebSocket;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Response;

namespace EasonEetwViewer.Dmdata.Caller.Services;

public class ApiCaller : IApiCaller
{
    private readonly HttpClient _client;
    private readonly AuthenticationWrapper _authenticatorDto;
    private readonly JsonSerializerOptions _options;

    private IAuthenticator Authenticator => _authenticatorDto.Authenticator;

    public ApiCaller(string baseApi, AuthenticationWrapper authenticator, JsonSerializerOptions jsonSerializerOptions)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(baseApi, nameof(baseApi));

        _client = new()
        {
            BaseAddress = new(baseApi)
        };
        _authenticatorDto = authenticator;
        _options = jsonSerializerOptions;
    }

    public async Task<ContractList> GetContractListAsync()
    {
        using HttpRequestMessage request = new(HttpMethod.Get, "contract");
        request.Headers.Authorization = await Authenticator.GetAuthenticationHeader();
        using HttpResponseMessage response = await _client.SendAsync(request);

        _ = response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        ContractList contractList = JsonSerializer.Deserialize<ContractList>(responseBody, _options) ?? throw new Exception();
        return contractList;
    }

    public async Task<WebSocketList> GetWebSocketListAsync(int? id = null, ConnectionStatus? connectionStatus = null, string? cursorToken = null, int? limit = null)
    {
        NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);
        if (id is not null)
        {
            queryString.Add("id", id.ToString());
        }

        if (connectionStatus is not null)
        {
            queryString.Add("status", ((ConnectionStatus)connectionStatus).ToUriString());
        }

        if (cursorToken is not null)
        {
            queryString.Add("cursorToken", cursorToken);
        }

        if (limit is not null)
        {
            queryString.Add("limit", limit.ToString());
        }

        using HttpRequestMessage request = new(HttpMethod.Get, $"socket?{queryString}");
        request.Headers.Authorization = await Authenticator.GetAuthenticationHeader();
        using HttpResponseMessage response = await _client.SendAsync(request);

        _ = response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        WebSocketList webSocketList = JsonSerializer.Deserialize<WebSocketList>(responseBody, _options) ?? throw new Exception();
        return webSocketList;
    }

    public async Task<WebSocketStart> PostWebSocketStartAsync(WebSocketStartPost postData)
    {
        string postDataJson = JsonSerializer.Serialize(postData);
        StringContent content = new(postDataJson, Encoding.UTF8, "application/json");
        using HttpRequestMessage request = new(HttpMethod.Post, "socket");
        request.Headers.Authorization = await Authenticator.GetAuthenticationHeader();
        request.Content = content;
        using HttpResponseMessage response = await _client.SendAsync(request);

        _ = response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        WebSocketStart startResponse = JsonSerializer.Deserialize<WebSocketStart>(responseBody, _options) ?? throw new Exception();
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

    public async Task<EarthquakeParameter> GetEarthquakeParameterAsync()
    {
        using HttpRequestMessage request = new(HttpMethod.Get, "parameter/earthquake/station");
        request.Headers.Authorization = await Authenticator.GetAuthenticationHeader();
        using HttpResponseMessage response = await _client.SendAsync(request);

        _ = response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        EarthquakeParameter earthquakeParameter = JsonSerializer.Deserialize<EarthquakeParameter>(responseBody, _options) ?? throw new Exception();
        return earthquakeParameter;
    }

    public async Task<GdEarthquakeList> GetPastEarthquakeListAsync(string? hypocentreCode = null, Intensity? maxInt = null, DateOnly? date = null, int? limit = null, string? cursorToken = null)
    {
        NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);
        if (hypocentreCode is not null)
        {
            queryString.Add("hypocenter", hypocentreCode);
        }

        if (maxInt is not null)
        {
            queryString.Add("maxInt", ((Intensity)maxInt).ToUriString());
        }

        if (date is not null)
        {
            queryString.Add("date", ((DateOnly)date).ToString("yyyy-MM-dd"));
        }

        if (limit is not null)
        {
            queryString.Add("limit", limit.ToString());
        }

        if (cursorToken is not null)
        {
            queryString.Add("cursorToken", cursorToken);
        }

        using HttpRequestMessage request = new(HttpMethod.Get, $"gd/earthquake?{queryString}");
        request.Headers.Authorization = await Authenticator.GetAuthenticationHeader();
        using HttpResponseMessage response = await _client.SendAsync(request);

        _ = response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        GdEarthquakeList pastEarthquakeList = JsonSerializer.Deserialize<GdEarthquakeList>(responseBody, _options) ?? throw new Exception();
        return pastEarthquakeList;
    }

    public async Task<GdEarthquakeEvent> GetPathEarthquakeEventAsync(string eventId)
    {
        using HttpRequestMessage request = new(HttpMethod.Get, $"gd/earthquake/{eventId}");
        request.Headers.Authorization = await Authenticator.GetAuthenticationHeader();
        using HttpResponseMessage response = await _client.SendAsync(request);

        _ = response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        GdEarthquakeEvent pastEarthquakeEvent = JsonSerializer.Deserialize<GdEarthquakeEvent>(responseBody, _options) ?? throw new Exception();
        return pastEarthquakeEvent;
    }
}
