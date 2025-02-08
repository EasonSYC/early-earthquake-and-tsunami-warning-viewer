using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Web;
using EasonEetwViewer.Authentication.Dtos;
using EasonEetwViewer.Authentication.Exceptions;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Authentication.Services;
/// <summary>
/// A helper which gets the refresh token and access token for OAuth 2.0.
/// </summary>
/// <param name="clientId">The client ID for OAuth.</param>
/// <param name="scopeString">The string containing the scopes to be requested.</param>
/// <param name="baseUri">The base URI of requests.</param>
/// <param name="host">The host specified in the requests.</param>
/// <param name="redirectPath">The relative path used to communicate grant codes.</param>
/// <param name="webPageString">The webpage contents to display after a successful authentication.</param>
internal sealed class OAuth2Helper(string clientId, string scopeString, string baseUri, string host, string redirectPath, string webPageString, ILogger<OAuth2Helper> logger)
{
    /// <summary>
    /// The length of a code verifier.
    /// </summary>
    private const int _codeLength = 64;
    /// <summary>
    /// The length of a state.
    /// </summary>
    private const int _stateLength = 32;
    /// <summary>
    /// The allowed characters when generating a State or a Code Verifier.
    /// </summary>
    private const string _allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    /// <summary>
    /// The HTTP Client to be used.
    /// </summary>
    private readonly HttpClient _client = new() { BaseAddress = new Uri(baseUri) };

    // Modified from https://nicolaiarocci.com/how-to-implement-pkce-code-challenge-in-csharp/
    /// <summary>
    /// Encodes a code verifier to a code challenge use SHA256 and Base 64 encoding.
    /// </summary>
    /// <param name="verifier">The code verifier.</param>
    /// <returns>The code challenge corresponding to the code verifier.</returns>
    private static string VerifierToChallenge(string verifier)
        => Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(verifier)))
            .Replace("+", "-")
            .Replace("/", "_")
            .TrimEnd('=');

    // https://stackoverflow.com/a/150974/
    /// <summary>
    /// Finds an unused port on the local IP address <c>localhost</c>.
    /// </summary>
    /// <returns>The port that is found to be unused/</returns>
    private static int GetUnusedPort()
    {
        TcpListener listener = new(IPAddress.Loopback, 0);
        listener.Start();
        int port = ((IPEndPoint)listener.LocalEndpoint).Port;
        listener.Stop();
        return port;
    }

    /// <summary>
    /// Generates a grant code query parameter with the given parameters.
    /// </summary>
    /// <param name="redirectUri">The redirect URI.</param>
    /// <param name="state">The state code.</param>
    /// <param name="codeVerifier">The code verifier.</param>
    /// <returns></returns>
    private NameValueCollection GenerateGrantCodeQueryParameters(Uri redirectUri, string state, string codeVerifier)
    {
        NameValueCollection queryParams = HttpUtility.ParseQueryString(string.Empty);
        queryParams["client_id"] = clientId;
        queryParams["response_type"] = "code";
        queryParams["redirect_uri"] = redirectUri.ToString();
        queryParams["response_mode"] = "query";
        queryParams["scope"] = scopeString;
        queryParams["state"] = state;
        queryParams["code_challenge"] = VerifierToChallenge(codeVerifier);
        queryParams["code_challenge_method"] = "S256";
        return queryParams;
    }

    /// <summary>
    /// Gets the grant code by executing on the relavant <see cref="Uri"/>s.
    /// </summary>
    /// <param name="redirectUri">The <see cref="Uri"/> to listen from.</param>
    /// <param name="browserUri">The <see cref="Uri"/> to redirect the user to.</param>
    /// <returns>A pair of grant code and the request state obtained.</returns>
    private async Task<(string grantCode, string requestState)> GetGrantCodeAsync(Uri redirectUri, Uri browserUri)
    {
        using HttpListener listener = new();
        listener.Prefixes.Add(redirectUri.ToString());
        listener.Start();

        // https://stackoverflow.com/a/61035650/
        _ = Process.Start(new ProcessStartInfo
        {
            FileName = browserUri.ToString(),
            UseShellExecute = true
        });

        HttpListenerContext context = await listener.GetContextAsync();
        Uri responseUri = context.Request.Url!;
        NameValueCollection query = HttpUtility.ParseQueryString(responseUri.Query);
        string grantCode = query["code"]!;
        string requestState = query["state"]!;

        byte[] buffer = Encoding.UTF8.GetBytes(webPageString);
        context.Response.ContentLength64 = buffer.Length;
        await context.Response.OutputStream.WriteAsync(buffer.AsMemory(0, buffer.Length));
        context.Response.OutputStream.Close();
        listener.Stop();

        return (grantCode, requestState);
    }
    /// <summary>
    /// Generates a refresh token request parameter with the given parameters.
    /// </summary>
    /// <param name="grantCode">The grant code.</param>
    /// <param name="redirectUri">The redirect URI.</param>
    /// <param name="codeVerifier">The code verifier.</param>
    /// <returns>A dictionary which represents the parameters to be used.</returns>
    private Dictionary<string, string> GenerateRefreshTokenRequestParameter(string grantCode, Uri redirectUri, string codeVerifier)
        => new(){
            { "client_id", clientId },
            { "grant_type", "authorization_code" },
            { "code", grantCode },
            { "redirect_uri", redirectUri.ToString() },
            { "code_verifier", codeVerifier}
        };
    /// <summary>
    /// Gets a new refresh token by prompting the user to login on a browser.
    /// </summary>
    /// <returns>A pair of tokens, the refresh token and the access token.</returns>
    /// <exception cref="OAuthSecurityException">When the state returned by the state is not in acoordance with the one sent.</exception>
    /// <exception cref="FormatException">When the returned data cannot be parsed to JSON successfully.</exception>
    public async Task<(string refreshToken, string accessToken)> GetRefreshTokenAsync()
    {
        string state = RandomNumberGenerator.GetString(_allowedChars, _stateLength);
        string codeVerifier = RandomNumberGenerator.GetString(_allowedChars, _codeLength);
        int port = GetUnusedPort();
        Uri redirectUri = new UriBuilder(IPAddress.Loopback.ToString())
        {
            Port = port,
            Path = redirectPath
        }.Uri;

        Uri browserUri = new UriBuilder(new Uri(new Uri(baseUri), "auth"))
        {
            Query = GenerateGrantCodeQueryParameters(redirectUri, state, codeVerifier).ToString()
        }
        .Uri;

        (string grantCode, string requestState) = await GetGrantCodeAsync(redirectUri, browserUri);

        if (requestState != state)
        {
            throw new OAuthSecurityException("The states do not match.");
        }

        Dictionary<string, string> requestParams = GenerateRefreshTokenRequestParameter(grantCode, redirectUri, codeVerifier);
        HttpRequestMessage request = OAuth2SharedMethod.GeneratePostRequest("token", requestParams, host);
        HttpResponseMessage response = await _client.SendAsync(request);
        _ = response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();
        TokenRequest token = JsonSerializer.Deserialize<TokenRequest>(responseBody) ?? throw new FormatException("The aquired data cannot be parsed to JSON successfully.");
        return (token.RefreshToken, token.AccessToken);
    }
}
