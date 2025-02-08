using System.Collections.Specialized;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using EasonEetwViewer.Authentication.Abstractions;
using EasonEetwViewer.Dmdata.Caller.Helpers;
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
    private readonly JsonSerializerOptions _options = new()
    {
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };

    private IAuthenticator Authenticator => _authenticatorDto.Authenticator;

    public ApiCaller(string baseApi, AuthenticationWrapper authenticator)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(baseApi, nameof(baseApi));

        _client = new()
        {
            BaseAddress = new(baseApi)
        };
        _authenticatorDto = authenticator;
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

    public async Task<WebSocketList> GetWebSocketListAsync(int id = -1, ConnectionStatus connectionStatus = ConnectionStatus.Unknown, string cursorToken = "", int limit = -1)
    {
        NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);
        if (id != -1)
        {
            queryString.Add("id", id.ToString());
        }

        if (connectionStatus != ConnectionStatus.Unknown)
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

    public async Task<GdEarthquakeList> GetPastEarthquakeListAsync(string hypocentreCode = "", Intensity maxInt = Intensity.Unknown, DateOnly date = new(), int limit = -1, string cursorToken = "")
    {
        NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);
        if (hypocentreCode != string.Empty)
        {
            queryString.Add("hypocenter", hypocentreCode);
        }

        if (maxInt != Intensity.Unknown)
        {
            queryString.Add("maxInt", maxInt.ToUriString());
        }

        if (date != new DateOnly())
        {
            queryString.Add("date", date.ToString("yyyy-MM-dd"));
        }

        if (limit != -1)
        {
            queryString.Add("limit", limit.ToString());
        }

        if (cursorToken != string.Empty)
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
