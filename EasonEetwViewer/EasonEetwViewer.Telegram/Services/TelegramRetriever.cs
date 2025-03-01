﻿using System.Net.Http.Headers;
using System.Net;
using EasonEetwViewer.Authentication.Abstractions;
using EasonEetwViewer.Telegram.Abstractions;
using EasonEetwViewer.Telegram.Dtos.TelegramBase;
using EasonEetwViewer.WebSocket.Exceptions;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Telegram.Services;
/// <summary>
/// The default implementation of <see cref="ITelegramRetriever"/>.
/// </summary>
public sealed class TelegramRetriever : ITelegramRetriever
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
    private readonly AuthenticationWrapper _authenticator;
    /// <summary>
    /// The logger to be used.
    /// </summary>
    private readonly ILogger<TelegramRetriever> _logger;
    /// <summary>
    /// Initializes a new instance of the <see cref="TelegramRetriever"/> class.
    /// </summary>
    /// <param name="baseApi">The base API of the telegram to be retrieved.</param>
    /// <param name="parser">The parser to be used to parse JSON telegrams.</param>
    /// <param name="authenticator">The authenticator to be used.</param>
    public TelegramRetriever(string baseApi, ITelegramParser parser, ILogger<TelegramRetriever> logger, AuthenticationWrapper authenticator)
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
            return null;
        }

        request.Headers.Authorization = authenticationHeaderValue;
        try
        {
            using HttpResponseMessage response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return _parser.ParseJsonTelegram(responseBody);
            }
            else
            {
                string error = await response.Content.ReadAsStringAsync();
                _logger.ApiErrorIgnored(error);

                if (response.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden)
                {
                    await _authenticator.InvalidAuthenticatorAsync(error);
                    return null;
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
