using System.Text.Json;
using System.Text.Json.Serialization;
using EasonEetwViewer.Authentication.Abstractions;
using EasonEetwViewer.Dmdata.Caller.Interfaces;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.TelegramBase;

namespace EasonEetwViewer.Dmdata.Caller.Services;
public class TelegramRetriever : ITelegramRetriever
{
    private readonly HttpClient _client;
    private readonly AuthenticationWrapper _authenticatorDto;
    private readonly JsonSerializerOptions _options;

    private IAuthenticator Authenticator => _authenticatorDto.Authenticator;

    public TelegramRetriever(string baseApi, AuthenticationWrapper authenticator, JsonSerializerOptions jsonSerializerOptions)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(baseApi, nameof(baseApi));

        _client = new()
        {
            BaseAddress = new(baseApi)
        };
        _authenticatorDto = authenticator;
        _options = jsonSerializerOptions;
    }

    public async Task<T> GetTelegramJsonAsync<T>(string id) where T : Head
    {
        using HttpRequestMessage request = new(HttpMethod.Get, $"{id}");
        request.Headers.Authorization = await Authenticator.GetAuthenticationHeader();
        using HttpResponseMessage response = await _client.SendAsync(request);

        _ = response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        T telegramData = JsonSerializer.Deserialize<T>(responseBody, _options) ?? throw new Exception();
        return telegramData;
    }
}
