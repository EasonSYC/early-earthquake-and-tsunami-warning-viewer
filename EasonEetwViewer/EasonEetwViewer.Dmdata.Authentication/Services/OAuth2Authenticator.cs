using System.Net.Http.Headers;
using System.Text.Json;
using EasonEetwViewer.Dmdata.Authentication.Abstractions;
using EasonEetwViewer.Dmdata.Authentication.Dtos;
using EasonEetwViewer.Dmdata.Authentication.Exceptions;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Dmdata.Authentication.Services;

/// <summary>
/// An implementation of <see cref="IAuthenticator"/>, using OAuth 2.0 to authenticate.
/// </summary>
internal sealed class OAuth2Authenticator : IAuthenticator
{
    /// <summary>
    /// The access token.
    /// </summary>
    private string _accessToken;
    /// <summary>
    /// The time of expiry for the access token.
    /// </summary>
    private DateTimeOffset _accessTokenExpiry;
    /// <summary>
    /// The time span for the validity of an access token.
    /// </summary>
    private readonly TimeSpan _accessTokenValidity = TimeSpan.FromHours(6);
    /// <summary>
    /// The refresh token to be used.
    /// </summary>
    private readonly string _refreshToken;
    /// <summary>
    /// The Client ID for OAuth 2.0
    /// </summary>
    private readonly string _clientId;
    /// <summary>
    /// The Host indicated in POST requests.
    /// </summary>
    private readonly string _host;
    /// <summary>
    /// The Base URI for requests.
    /// </summary>
    private readonly Uri _base;
    /// <summary>
    /// The HttpClient used for requests.
    /// </summary>
    private readonly HttpClient _httpClient;
    /// <summary>
    /// The logger to be used for logging.
    /// </summary>
    private readonly ILogger<OAuth2Authenticator> _logger;
    /// <summary>
    /// Creates a new instance of the class with a set of given parameters.
    /// </summary>
    /// <param name="clientId">The Client ID for OAuth 2.0.</param>
    /// <param name="basePath">The Base Path for HTTP requests.</param>
    /// <param name="host">The Host indicated in POST requests.</param>
    /// <param name="refreshToken">The refresh token to be used.</param>
    /// <param name="accessToken">The access token to be used, or <see langword="null"/> if unprovided.</param>
    /// <param name="logger">The logger to be used.</param>
    public OAuth2Authenticator(string clientId, string basePath, string host, string refreshToken, string? accessToken, ILogger<OAuth2Authenticator> logger)
    {
        if (!refreshToken.StartsWith("ARh."))
        {
            throw new ArgumentException($"{refreshToken} does not match format of refresh token.", nameof(refreshToken));
        }

        if (accessToken is not null && !accessToken.StartsWith("ATn."))
        {
            throw new ArgumentException($"{accessToken} does not match format of access token.", nameof(accessToken));
        }

        _clientId = clientId;
        _host = host;
        _base = new(basePath);
        _httpClient = new()
        {
            BaseAddress = _base
        };
        _refreshToken = refreshToken;
        _logger = logger;
        _accessToken = accessToken ?? Task.Run(RenewAccessTokenAsync).Result;
        _accessTokenExpiry = DateTimeOffset.Now + _accessTokenValidity;
        _logger.Instantiated();
    }

    /// <inheritdoc/>
    public async Task<AuthenticationHeaderValue> GetAuthenticationHeaderAsync()
        => new("Bearer", await CheckAccessTokenAsync());

    /// <inheritdoc/>
    public override string ToString()
        => _refreshToken;

    /// <summary>
    /// Checks if the access token if valid, and refresh its validity where necessary.
    /// </summary>
    /// <returns>A <see cref="Task"/> object that represents the asynchronous operation.</returns>
    public async Task<string> CheckAccessTokenAsync()
        => _accessTokenExpiry > DateTimeOffset.Now
            ? _accessToken
            : await RenewAccessTokenAsync();

    /// <summary>
    /// Gets a new access token.
    /// </summary>
    /// <returns>A <see cref="Task"/> object that represents the asynchronous operation.</returns>
    /// <exception cref="OAuthJsonException">When the returned data cannot be parsed to JSON successfully.</exception>
    /// <exception cref="OAuthErrorException">When there is an error in the response.</exception>
    private async Task<string> RenewAccessTokenAsync()
    {
        if (_accessToken is not null)
        {
            try
            {
                await RevokeTokenAsync(_accessToken);
            }
            catch (Exception ex)
            {
                _logger.IgnoredException(ex.ToString());
            }

            _accessTokenExpiry = DateTimeOffset.MinValue;
        }

        _logger.RequestingNewAccessToken();
        Dictionary<string, string> requestParams = new(){
            { "client_id", _clientId },
            { "grant_type", "refresh_token" },
            { "refresh_token", _refreshToken }
        };
        HttpRequestMessage request = OAuth2SharedMethod.GeneratePostRequest("token", requestParams, _host);

        HttpResponseMessage response = await _httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            try
            {
                TokenRefresh token = JsonSerializer.Deserialize<TokenRefresh>(responseBody)
                    ?? throw new OAuthJsonException($"Cannot deserialise: {responseBody}");
                _accessTokenExpiry = DateTimeOffset.Now + _accessTokenValidity;
                _accessToken = token.AccessToken;
                _logger.NewAccessTokenAcquired();
                return _accessToken;
            }
            catch (OAuthJsonException)
            {
                _logger.IncorrectJsonFormat(responseBody);
                throw;
            }
            catch (JsonException ex)
            {
                _logger.IncorrectJsonFormat(responseBody);
                throw new OAuthJsonException($"Cannot deserialise: {responseBody}", ex);
            }
        }
        else
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            try
            {
                Error error = JsonSerializer.Deserialize<Error>(responseBody)
                    ?? throw new OAuthJsonException($"Cannot deserialise: {responseBody}");
                _logger.TokenRevokeFailed(error.Description);
                throw new OAuthErrorException($"{error.Short} {error.Description}");
            }
            catch (OAuthJsonException)
            {
                _logger.IncorrectJsonFormat(responseBody);
                throw;
            }
            catch (JsonException ex)
            {
                _logger.IncorrectJsonFormat(responseBody);
                throw new OAuthJsonException($"Cannot deserialise: {responseBody}", ex);
            }
        }
    }

    /// <summary>
    /// Revokes the specified token.
    /// </summary>
    /// <param name="token">The token to be revoked.</param>
    /// <returns>A <see cref="Task"/> object that represents the asynchronous operation.</returns>
    /// <exception cref="OAuthJsonException">When the returned data cannot be parsed to JSON successfully.</exception>
    /// <exception cref="OAuthErrorException">When there is an error in the response.</exception>
    private async Task RevokeTokenAsync(string token)
    {
        Dictionary<string, string> requestParams = new(){
            { "client_id", _clientId },
            { "token", token }
        };
        _logger.RevokingToken(token);
        HttpRequestMessage request = OAuth2SharedMethod.GeneratePostRequest("revoke", requestParams, _host);
        HttpResponseMessage response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            _logger.TokenRevoked();
        }
        else
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            try
            {

                Error error = JsonSerializer.Deserialize<Error>(responseBody)
                    ?? throw new OAuthJsonException($"Cannot deserialise: {responseBody}");
                _logger.TokenRevokeFailed(error.Description);
                throw new OAuthErrorException($"{error.Short} {error.Description}");
            }
            catch (OAuthJsonException)
            {
                _logger.IncorrectJsonFormat(responseBody);
                throw;
            }
            catch (JsonException ex)
            {
                _logger.IncorrectJsonFormat(responseBody);
                throw new OAuthJsonException($"Cannot deserialise: {responseBody}", ex);
            }
        }
    }

    /// <summary>
    /// Revokes the access token and the refresh token.
    /// </summary>
    /// <returns>A <see cref="Task"/> object that represents the asynchronous operation.</returns>
    public async Task RevokeTokens()
    {
        Task revokeAccessToken = RevokeTokenAsync(_accessToken);
        Task revokeRefreshToken = RevokeTokenAsync(_refreshToken);
        await Task.WhenAll(revokeAccessToken, revokeRefreshToken);
    }
}