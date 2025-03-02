using System.Net;
using System.Net.Http.Headers;
using EasonEetwViewer.Dmdata.Authentication.Abstractions;
using EasonEetwViewer.Dmdata.Telegram.Abstractions;
using EasonEetwViewer.Dmdata.Telegram.Dtos.TelegramBase;
using EasonEetwViewer.Dmdata.Telegram.Exceptions;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Dmdata.Telegram.Services;
/// <summary>
/// The default implementation of <see cref="ITelegramRetriever"/>.
/// </summary>
internal sealed class TelegramRetriever : ITelegramRetriever
{
    /// <summary>
    /// The <see cref="HttpClient"/> to be used.
    /// </summary>
    private readonly HttpClient _client;
    /// <summary>
    /// The parser for JSON to be used.
    /// </summary>
    private readonly ITelegramParser _parser;
    /// <summary>
    /// The authenticator to be used.
    /// </summary>
    private readonly IAuthenticationHelper _authenticator;
    /// <summary>
    /// The logger to be used.
    /// </summary>
    private readonly ILogger<TelegramRetriever> _logger;
    /// <summary>
    /// Initializes a new instance of the <see cref="TelegramRetriever"/> class.
    /// </summary>
    /// <param name="baseApi">The base API of the telegram to be retrieved.</param>
    /// <param name="parser">The parser to be used to parse JSON telegrams.</param>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="authenticator">The authenticator to be used.</param>
    public TelegramRetriever(string baseApi, ITelegramParser parser, ILogger<TelegramRetriever> logger, IAuthenticationHelper authenticator)
    {
        _client = new()
        {
            BaseAddress = new(baseApi)
        };
        _authenticator = authenticator;
        _parser = parser;
        _logger = logger;
    }
    /// <inheritdoc/>
    public async Task<Head?> GetJsonTelegramAsync(string id)
    {
        using HttpRequestMessage request = new(HttpMethod.Get, $"{id}");
        AuthenticationHeaderValue? authenticationHeaderValue = await _authenticator.GetAuthenticationHeaderAsync();
        if (authenticationHeaderValue is null)
        {
            _logger.NotAuthenticated();
            return null;
        }

        request.Headers.Authorization = authenticationHeaderValue;
        try
        {
            _logger.Requesting(id);
            using HttpResponseMessage response = await _client.SendAsync(request);
            _logger.Requested(id);
            string responseBody = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return _parser.ParseJsonTelegram(responseBody);
            }
            else
            {
                _logger.ApiErrorIgnored(responseBody);

                if (response.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden)
                {
                    await _authenticator.InvalidAuthenticatorAsync(responseBody);
                }

                return null;
            }
        }
        catch (TelegramParserException ex)
        {
            _logger.ParserExceptionIgnored(ex.ToString());
            return null;
        }
    }
}
