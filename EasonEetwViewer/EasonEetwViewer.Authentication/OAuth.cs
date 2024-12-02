using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Web;
using EasonEetwViewer.Authentication.OAuth2;
using EasonEetwViewer.Authentication.OAuth2.Dto;

namespace EasonEetwViewer.Authentication;

/// <summary>
/// An implementation of <c>IAuthenticator</c>, using OAuth 2.0 to authenticate.
/// </summary>
public class OAuth : IAuthenticator
{
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
    /// The token data for OAuth 2.0.
    /// </summary>
    private readonly TokenData _tokenData;
    /// <summary>
    /// The allowed characters when generating a State or a Code Verifier.
    /// </summary>
    private const string _allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    /// <summary>
    /// The webpage contents to display after a successful authentication.
    /// </summary>
    private const string _webpageString = "<html><body><h1>You may close this window.</h1></body></html>";
    /// <summary>
    /// The relative path used to communicate grant codes.
    /// </summary>
    private const string _redirectPath = "eeetwv-code-auth/";
    /// <summary>
    /// The directory to store tokens to be retrieved in the next run.
    /// </summary>
    private const string _tokenPath = "oAuth.json";
    /// <summary>
    /// The length of a code verifier.
    /// </summary>
    private const int _codeLength = 64;
    /// <summary>
    /// The length of a state.
    /// </summary>
    private const int _stateLength = 32;
    /// <summary>
    /// The time for the validity of an access token.
    /// </summary>
    private readonly TimeSpan _accessTokenValidity = TimeSpan.FromHours(6);
    /// <summary>
    /// The time for the validity of a refresh token.
    /// </summary>
    private readonly TimeSpan _refreshTokenValidity = TimeSpan.FromDays(183);
    /// <summary>
    /// Creates a new instance of the class with a set of given parameters.
    /// </summary>
    /// <param name="clientId">The Client ID for OAuth 2.0.</param>
    /// <param name="basePath">The Base Path for HTTP requests.</param>
    /// <param name="host">The Host indicated in POST requests.</param>
    /// <param name="scopes">A list of strings indicating the scopes of the API Keys.</param>
    public OAuth(string clientId, string basePath, string host, HashSet<string> scopes)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(clientId, nameof(clientId));
        ArgumentException.ThrowIfNullOrWhiteSpace(basePath, nameof(basePath));
        ArgumentException.ThrowIfNullOrWhiteSpace(host, nameof(host));

        _clientId = clientId;
        _host = host;
        _base = new(basePath);
        _httpClient = new()
        {
            BaseAddress = _base
        };

        TokenData? readData = ReadToken(_tokenPath);
        if (readData is null)
        {
            _tokenData = new(scopes, _accessTokenValidity, _refreshTokenValidity);
            WriteToken(_tokenPath);
        }
        else if (readData.Scopes.SetEquals(scopes)
            && readData.AccessToken.Validity == _accessTokenValidity
            && readData.RefreshToken.Validity == _refreshTokenValidity)
        {
            _tokenData = readData;
            WriteToken(_tokenPath);
        }
        else
        {
            _tokenData = new(scopes, _accessTokenValidity, _refreshTokenValidity);
            WriteToken(_tokenPath);
        }
    }
    private static TokenData? ReadToken(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return null;
        }

        try
        {
            return JsonSerializer.Deserialize<TokenData>(File.ReadAllText(filePath));
        }
        catch
        {
            return null;
        }
    }

    private void WriteToken(string filePath) => File.WriteAllText(filePath, JsonSerializer.Serialize(_tokenData));

    private static string VerifierToChallenge(string verifier) =>
        Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(verifier)))
        .Replace("+", "-")
        .Replace("/", "_")
        .TrimEnd('=');

    private void FindUnusedPort()
    {
        TcpListener listener = new(IPAddress.Loopback, 0);
        listener.Start();
        int port = ((IPEndPoint)listener.LocalEndpoint).Port;
        listener.Stop();

        _tokenData.Port = port;
    }

    /// <summary>
    /// Returns an <c>AuthenticationHeaderValue</c> to be used in a HTTP request.
    /// This method used the stored access key, if possible.
    /// </summary>
    /// <returns>An <c>AuthenticationHeaderValue</c> using <c>Bearer</c>.</returns>
    public async Task<AuthenticationHeaderValue> GetAuthenticationHeader()
    {
        await CheckAccessTokenAsync();
        return new("Bearer", _tokenData.AccessToken.Code);
    }

    /// <summary>
    /// Returns an <c>AuthenticationHeaderValue</c> to be used in a HTTP request.
    /// This method always gets a new of access token.
    /// </summary>
    /// <returns>An <c>AuthenticationHeaderValue</c> using <c>Bearer</c>.</returns>
    public async Task<AuthenticationHeaderValue> GetNewAuthenticationHeader()
    {
        await NewAccessTokenAsync();
        return new("Bearer", _tokenData.AccessToken.Code);
    }

    private async Task CheckAccessTokenAsync()
    {
        if (_tokenData.AccessToken.IsValid)
        {
            return;
        }

        if (_tokenData.RefreshToken.IsValid)
        {
            await RenewAccessTokenAsync();
            return;
        }

        await RenewRefreshTokenAsync();
        return;
    }

    private async Task NewAccessTokenAsync()
    {
        Task revokeAccessToken = RevokeTokenRequestAsync(_tokenData.AccessToken.Code);
        _tokenData.AccessToken.Reset();
        WriteToken(_tokenPath);

        Task checkNewAccessToken = CheckAccessTokenAsync();
        await Task.WhenAll(revokeAccessToken, checkNewAccessToken);
        return;
    }
    private async Task<string> GenerateGrantCode()
    {
        string state = RandomNumberGenerator.GetString(_allowedChars, _stateLength);
        _tokenData.CodeVerifier = RandomNumberGenerator.GetString(_allowedChars, _codeLength);

        FindUnusedPort();
        Uri redirectUri = new UriBuilder(IPAddress.Loopback.ToString())
        {
            Port = _tokenData.Port,
            Path = _redirectPath
        }.Uri;

        NameValueCollection? queryParams = HttpUtility.ParseQueryString(string.Empty);
        queryParams["client_id"] = _clientId;
        queryParams["response_type"] = "code";
        queryParams["redirect_uri"] = redirectUri.ToString();
        queryParams["response_mode"] = "query";
        queryParams["scope"] = _tokenData.ScopeString;
        queryParams["state"] = state;
        queryParams["code_challenge"] = VerifierToChallenge(_tokenData.CodeVerifier);
        queryParams["code_challenge_method"] = "S256";

        Uri browserUri = new UriBuilder(new Uri(_base, "auth"))
        {
            Query = queryParams.ToString()
        }.Uri;

        using HttpListener listener = new();
        listener.Prefixes.Add(redirectUri.ToString());
        listener.Start();

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

        byte[] buffer = Encoding.UTF8.GetBytes(_webpageString);
        context.Response.ContentLength64 = buffer.Length;
        await context.Response.OutputStream.WriteAsync(buffer.AsMemory(0, buffer.Length));
        context.Response.OutputStream.Close();
        listener.Stop();

        return requestState != state ? throw new Exception() : grantCode;
    }
    private async Task RenewRefreshTokenAsync()
    {
        Task revokeAccessToken = RevokeTokenRequestAsync(_tokenData.AccessToken.Code);
        Task revokeRefreshToken = RevokeTokenRequestAsync(_tokenData.RefreshToken.Code);
        _tokenData.AccessToken.Reset();
        _tokenData.RefreshToken.Reset();
        WriteToken(_tokenPath);

        string grantCode = await GenerateGrantCode();

        Dictionary<string, string> requestParams = new(){
            { "client_id", _clientId },
            { "grant_type", "authorization_code" },
            { "code", grantCode },
            { "redirect_uri", new UriBuilder(IPAddress.Loopback.ToString())
                {
                    Port = _tokenData.Port,
                    Path = _redirectPath
                }.ToString()},
            { "code_verifier", _tokenData.CodeVerifier}
        };
        HttpRequestMessage request = GeneratePostRequest("token", requestParams);

        HttpResponseMessage response = await _httpClient.SendAsync(request);
        _ = response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();
        TokenRequest token = JsonSerializer.Deserialize<TokenRequest>(responseBody) ?? throw new Exception();

        _tokenData.ResetValidity();
        _tokenData.AccessToken.Code = token.AccessToken;
        _tokenData.RefreshToken.Code = token.RefreshToken;
        WriteToken(_tokenPath);

        await Task.WhenAll(revokeAccessToken, revokeRefreshToken);
    }
    private async Task RenewAccessTokenAsync()
    {

        Task revokeAccessToken = RevokeTokenRequestAsync(_tokenData.AccessToken.Code);
        _tokenData.AccessToken.Reset();
        WriteToken(_tokenPath);

        Dictionary<string, string> requestParams = new(){
            { "client_id", _clientId },
            { "grant_type", "refresh_token" },
            { "refresh_token", _tokenData.RefreshToken.Code }
        };
        HttpRequestMessage request = GeneratePostRequest("token", requestParams);

        HttpResponseMessage response = await _httpClient.SendAsync(request);
        _ = response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();
        TokenRefresh token = JsonSerializer.Deserialize<TokenRefresh>(responseBody) ?? throw new Exception();

        _tokenData.ResetValidity();
        _tokenData.AccessToken.Code = token.AccessToken;
        WriteToken(_tokenPath);

        await revokeAccessToken;
    }
    private async Task RevokeTokenRequestAsync(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return;
        }

        Dictionary<string, string> requestParams = new(){
            { "client_id", _clientId },
            { "token", token }
        };
        HttpRequestMessage request = GeneratePostRequest("revoke", requestParams);
        HttpResponseMessage response = await _httpClient.SendAsync(request);
        _ = response.EnsureSuccessStatusCode();
        _ = await response.Content.ReadAsStringAsync();
    }
    private HttpRequestMessage GeneratePostRequest(string requestUri, Dictionary<string, string> requestParams)
    {
        FormUrlEncodedContent content = new(requestParams);
        HttpRequestMessage request = new(HttpMethod.Post, requestUri)
        {
            Content = content
        };
        request.Headers.Host = _host.ToString();
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

        return request;
    }
}