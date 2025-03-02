using System.Collections.Specialized;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Web;
using EasonEetwViewer.Api.Abstractions;
using EasonEetwViewer.Api.Dtos.Enum.WebSocket;
using EasonEetwViewer.Api.Dtos.Request;
using EasonEetwViewer.Api.Dtos.Response;
using EasonEetwViewer.Api.Extensions;
using EasonEetwViewer.Api.Logging;
using EasonEetwViewer.Authentication.Abstractions;
using EasonEetwViewer.Dtos.Enum;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Api.Services;
/// <summary>
/// The default implmenetation for <see cref="IApiCaller"/>.
/// </summary>
internal sealed class ApiCaller : IApiCaller
{
    private readonly HttpClient _client;
    private readonly ILogger<ApiCaller> _logger;
    private readonly IAuthenticationHelper _authenticator;
    private readonly JsonSerializerOptions _options;
    private static NameValueCollection EmptyQuery
        => HttpUtility.ParseQueryString(string.Empty);
    public ApiCaller(string baseApi, ILogger<ApiCaller> logger, IAuthenticationHelper authenticator, JsonSerializerOptions jsonSerializerOptions)
    {
        _client = new()
        {
            BaseAddress = new(baseApi)
        };
        _authenticator = authenticator;
        _options = jsonSerializerOptions;
        _logger = logger;
        _logger.Instantiated();
    }
    /// <inheritdoc/>
    public async Task<ContractList?> GetContractListAsync()
        => await ParseGetResultAsync<ContractList>("contract");
    /// <inheritdoc/>
    public async Task<WebSocketList?> GetWebSocketListAsync(
        int? id = null,
        ConnectionStatus? connectionStatus = null,
        string? cursorToken = null,
        int? limit = null)
        => await ParseGetResultAsync<WebSocketList>("socket",
            EmptyQuery
            .AddIfNotNull(id, "id")
            .AddIfNotNull(connectionStatus, "status", x => x?.ToUriString())
            .AddIfNotNull(cursorToken, "cursorToken")
            .AddIfNotNull(limit, "limit")
            .ToString());
    /// <inheritdoc/>
    public async Task<WebSocketStart?> PostWebSocketStartAsync(WebSocketStartPost postData)
        => await ParsePostJsonAsync<WebSocketStart, WebSocketStartPost>(
            "socket",
            postData);
    /// <inheritdoc/>
    public async Task<bool> DeleteWebSocketAsync(int id)
        => await ParseDeleteResultAsync($"socket/{id}");
    /// <inheritdoc/>
    public async Task<EarthquakeParameter?> GetEarthquakeParameterAsync()
        => await ParseGetResultAsync<EarthquakeParameter>("parameter/earthquake/station");
    /// <inheritdoc/>
    public async Task<GdEarthquakeList?> GetPastEarthquakeListAsync(
        string? hypocentreCode = null,
        Intensity? maxInt = null,
        DateOnly? date = null,
        int? limit = null,
        string? cursorToken = null)
        => await ParseGetResultAsync<GdEarthquakeList>("gd/earthquake",
            EmptyQuery
            .AddIfNotNull(hypocentreCode, "hypocenter")
            .AddIfNotNull(maxInt, "maxInt", x => x?.ToUriString())
            .AddIfNotNull(date, "date", x => x?.ToString("yyyy-MM-dd"))
            .AddIfNotNull(limit, "limit")
            .AddIfNotNull(cursorToken, "cursorToken")
            .ToString());
    /// <inheritdoc/>
    public async Task<GdEarthquakeEvent?> GetPathEarthquakeEventAsync(string eventId)
        => await ParseGetResultAsync<GdEarthquakeEvent>($"gd/earthquake/{eventId}");
    private async Task<T?> ParseGetResultAsync<T>(string relativePath, string? queryString) where T : class
        => await ParseGetResultAsync<T>(
            string.IsNullOrEmpty(queryString)
            ? relativePath
            : $"{relativePath}?{queryString}");
    /// <summary>
    /// Parses the result of a GET request.
    /// </summary>
    /// <param name="relativePath">The relative path to the request.</param>
    /// <returns>The result if successful, <see langword="null"/> otherwise.</returns>
    private async Task<T?> ParseGetResultAsync<T>(string relativePath) where T : class
    {
        using HttpRequestMessage request = CreateHttpRequest(HttpMethod.Get, relativePath);
        return await ParseResultAsync<T>(request);
    }
    /// <summary>
    /// Parses the result of a POST request.
    /// </summary>
    /// <param name="relativePath">The relative path to the request.</param>
    /// <param name="content">The content to be included in the request.</param>
    /// <returns>The result if successful, <see langword="null"/> otherwise.</returns>
    private async Task<TResult?> ParsePostJsonAsync<TResult, TContent>(string relativePath, TContent content) where TResult : class
    {
        using HttpRequestMessage request = CreateHttpRequest(
            HttpMethod.Post,
            relativePath,
            new StringContent(
                JsonSerializer.Serialize(content),
                Encoding.UTF8,
                "application/json"));
        return await ParseResultAsync<TResult>(request);
    }
    /// <summary>
    /// Parses the result of a DELETE request.
    /// </summary>
    /// <param name="relativePath">The relative path to the request.</param>
    /// <returns><see langword="true"/> if successful, <see langword="false"/> otherwise.</returns>
    private async Task<bool> ParseDeleteResultAsync(string relativePath)
    {
        using HttpRequestMessage request = CreateHttpRequest(HttpMethod.Delete, relativePath);
        return await ParseSuccessAsync(request);
    }
    /// <summary>
    /// Parses the result of the request.
    /// </summary>
    /// <typeparam name="T">The type to parse the result into</typeparam>
    /// <param name="request">The <see cref="HttpRequestMessage"/> to be sent.</param>
    /// <returns>The result if successful, <see langword="null"/> otherwise.</returns>
    private async Task<T?> ParseResultAsync<T>(HttpRequestMessage request) where T : class
    {
        using HttpResponseMessage? response = await SendRequestAsync(request);
        if (response is null)
        {
            return null;
        }

        string responseBody = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            try
            {
                T? result = JsonSerializer.Deserialize<T>(responseBody, _options);

                if (result is null)
                {
                    _logger.JsonParsingFailed(responseBody);
                    return null;
                }

                return result;
            }
            catch (JsonException)
            {
                _logger.JsonParsingFailed(responseBody);
                return null;
            }
        }
        else
        {
            await HandleError(responseBody, response.StatusCode);
            return null;
        }
    }
    /// <summary>
    /// Parses the result of the request.
    /// </summary>
    /// <param name="request">The <see cref="HttpRequestMessage"/> to be sent.</param>
    /// <returns><see langword="true"/> if successful, <see langword="false"/> otherwise.</returns>
    private async Task<bool> ParseSuccessAsync(HttpRequestMessage request)
    {
        using HttpResponseMessage? response = await SendRequestAsync(request);
        if (response is null)
        {
            return false;
        }
        else if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            await HandleError(responseBody, response.StatusCode);
            return false;
        }
    }
    /// <summary>
    /// Handles the error received from the API and logs as appropriate.
    /// </summary>
    /// <param name="responseBody"></param>
    /// <param name="statusCode"></param>
    /// <returns>A <see cref="Task"/> object that represents the asynchronous operation.</returns>
    private async Task HandleError(string responseBody, HttpStatusCode statusCode)
    {
        try
        {
            Error? error = JsonSerializer.Deserialize<Error>(responseBody, _options);
            if (error is null)
            {
                _logger.JsonParsingFailed(responseBody);
                return;
            }

            _logger.ErrorReceived(error.ErrorDetails.Code, error.ErrorDetails.Message);

            if (statusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden)
            {
                await _authenticator.InvalidAuthenticatorAsync(error.ErrorDetails.Message);
            }
        }
        catch (JsonException)
        {
            _logger.JsonParsingFailed(responseBody);
        }
    }
    /// <summary>
    /// Adds the authentication header to the request and sends it.
    /// </summary>
    /// <param name="request">The <see cref="HttpRequestMessage"/> to be sent.</param>
    /// <returns>The <see cref="HttpResponseMessage"/> obtained. <see langword="null"/> when unsuccessful.</returns>
    private async Task<HttpResponseMessage?> SendRequestAsync(HttpRequestMessage request)
    {
        AuthenticationHeaderValue? header = await _authenticator.GetAuthenticationHeaderAsync();
        if (header is null)
        {
            _logger.NotAuthenticated();
            return null;
        }

        request.Headers.Authorization = header;
        try
        {
            return await _client.SendAsync(request);
        }
        catch (HttpRequestException ex)
        {
            _logger.HttpRequestFails(ex.ToString());
            return null;
        }
    }
    /// <summary>
    /// Creates a new <see cref="HttpRequestMessage"/> with the specified HTTP Method and relative path.
    /// </summary>
    /// <param name="method">The <see cref="HttpMethod"/> specified.</param>
    /// <param name="relativePath">The relative path.</param>
    /// <returns>The <see cref="HttpRequestMessage"/> created.</returns>
    private HttpRequestMessage CreateHttpRequest(HttpMethod method, string relativePath)
    {
        _logger.RequestCreated(method, relativePath);
        return new(method, relativePath);
    }
    /// <summary>
    /// Creates a new <see cref="HttpRequestMessage"/> with the specified HTTP Method, relative path and content.
    /// </summary>
    /// <param name="method">The <see cref="HttpMethod"/> specified.</param>
    /// <param name="relativePath">The relative path.</param>
    /// <param name="content">The <see cref="HttpContent"/> to be included.</param>
    /// <returns>The <see cref="HttpRequestMessage"/> created.</returns>
    private HttpRequestMessage CreateHttpRequest(HttpMethod method, string relativePath, HttpContent content)
    {
        _logger.RequestWithContentCreated(method, relativePath, content);
        return new(method, relativePath) { Content = content };
    }
}
