using System;
using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Web;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using EasonEetwViewer.Authentication.OAuth2;

namespace EasonEetwViewer.Authentication;

public class OAuth : IAuthenticator
{
    private readonly string _clientId;
    private readonly string _host;
    private readonly Uri _base;
    private readonly HttpClient _httpClient;
    private TokenSet _tokens;
    private readonly string _scopes;
    private const string _allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    private const string _redirectPath = "eeetwv-code-auth/";
    private const string _tokenPath = "oauth.json";
    private const int _codeLength = 64;
    private const int _stateLength = 8;
    private const int _accessTokenValiditySeconds = 21600;
    private const int _refreshTokenValidityDays = 183;
    public OAuth(string clientId, string basePath, string host, string scopes)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(clientId, nameof(clientId));
        ArgumentException.ThrowIfNullOrWhiteSpace(basePath, nameof(basePath));
        ArgumentException.ThrowIfNullOrWhiteSpace(host, nameof(host));
        ArgumentException.ThrowIfNullOrWhiteSpace(scopes, nameof(scopes));

        _clientId = clientId;
        _host = host;
        _base = new(basePath);
        _httpClient = new()
        {
            BaseAddress = _base
        };
        _scopes = scopes;
        _tokens = ReadToken(_tokenPath);
    }
    private static TokenSet ReadToken(string filePath)
    {
        if (File.Exists(filePath))
        {
            string fileBody = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<TokenSet>(fileBody) ?? TokenSet.Default;   
        }

        return TokenSet.Default;
    }
    private void WriteToken(string filePath)
    {
        string fileBody = JsonSerializer.Serialize<TokenSet>(_tokens);
        File.WriteAllText(filePath, fileBody);
        Console.WriteLine(fileBody);
    }
    private static string VerifierToChallenge(string verifier)
    {
        byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(verifier));
        string challenge = Convert.ToBase64String(hash).Replace("+", "-").Replace("/", "_").TrimEnd('=');
        return challenge;
    }
    private void FindUnusedPort()
    {
        TcpListener listener = new(IPAddress.Loopback, 0);
        listener.Start();
        int port = ((IPEndPoint)listener.LocalEndpoint).Port;
        listener.Stop();

        _tokens.Port = port;
    }
    public async Task<string> GetAuthenticationHeader()
    {
        await CheckAccessTokenAsync();
        return $"Bearer {_tokens.AccessToken.Code}";
    }
    public async Task<string> GetNewAuthenticationHeader()
    {
        await NewAccessTokenAsync();
        return $"Bearer {_tokens.AccessToken.Code}";
    }
    public async Task CheckAccessTokenAsync()
    {
        if (_tokens.AccessToken.IsValid)
        {
            return;
        }
        if (_tokens.RefreshToken.IsValid)
        {
            await RenewAccessTokenAsync();
            return;
        }
        await RenewRefreshTokenAsync();
        return;
    }
    public async Task NewAccessTokenAsync()
    {
        Task revokeAccessToken = RevokeTokenRequestAsync(_tokens.AccessToken.Code);
        _tokens.AccessToken = Token.Default;
        WriteToken(_tokenPath);

        Task checkNewAccessToken = CheckAccessTokenAsync();
        await Task.WhenAll(revokeAccessToken, checkNewAccessToken);
        return;
    }
    private async Task<string> GenerateGrantCode()
    {
        string state = RandomNumberGenerator.GetString(_allowedChars, _stateLength);
        _tokens.CodeVerifier = RandomNumberGenerator.GetString(_allowedChars, _codeLength);

        FindUnusedPort();
        Uri redirectUri = new UriBuilder(IPAddress.Loopback.ToString())
        {
            Port = _tokens.Port,
            Path = _redirectPath
        }.Uri;

        NameValueCollection? queryParams = HttpUtility.ParseQueryString(string.Empty);
        queryParams["client_id"] = _clientId;
        queryParams["response_type"] = "code";
        queryParams["redirect_uri"] = redirectUri.ToString();
        queryParams["response_mode"] = "query";
        queryParams["scope"] = _scopes;
        queryParams["state"] = state;
        queryParams["code_challenge"] = VerifierToChallenge(_tokens.CodeVerifier);
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

        string webpageString = "<html><body><h1>You may close this window.</h1></body></html>";
        byte[] buffer = Encoding.UTF8.GetBytes(webpageString);
        context.Response.ContentLength64 = buffer.Length;
        await context.Response.OutputStream.WriteAsync(buffer.AsMemory(0, buffer.Length));
        context.Response.OutputStream.Close();
        listener.Stop();

        return requestState != state ? throw new Exception() : grantCode;
    }
    private async Task RenewRefreshTokenAsync()
    {
        Task revokeAccessToken = RevokeTokenRequestAsync(_tokens.AccessToken.Code);
        Task revokeRefreshToken = RevokeTokenRequestAsync(_tokens.RefreshToken.Code);
        _tokens = TokenSet.Default;
        WriteToken(_tokenPath);

        string grantCode = await GenerateGrantCode();

        Dictionary<string, string> requestParams = new(){
            { "client_id", _clientId },
            { "grant_type", "authorization_code" },
            { "code", grantCode },
            { "redirect_uri", new UriBuilder(IPAddress.Loopback.ToString())
            {
                Port = _tokens.Port,
                Path = _redirectPath
                }.ToString()},
            { "code_verifier", _tokens.CodeVerifier}
        };
        HttpRequestMessage request = GeneratePostRequest("token", requestParams);

        DateTime accessTokenExpiry = DateTime.Now.Add(TimeSpan.FromSeconds(_accessTokenValiditySeconds));
        DateTime refreshTokenExpiry = DateTime.Now.Add(TimeSpan.FromDays(_refreshTokenValidityDays));

        HttpResponseMessage response = await _httpClient.SendAsync(request);
        _ = response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();
        TokenResponse token = JsonSerializer.Deserialize<TokenResponse>(responseBody) ?? throw new Exception();

        _tokens.RefreshToken = new() { Code = token.RefreshToken, Expiry = refreshTokenExpiry };
        _tokens.AccessToken = new() { Code = token.AccessToken, Expiry = accessTokenExpiry };
        WriteToken(_tokenPath);

        await Task.WhenAll(revokeAccessToken, revokeRefreshToken);
    }
    private async Task RenewAccessTokenAsync()
    {

        Task revokeAccessToken = RevokeTokenRequestAsync(_tokens.AccessToken.Code);
        _tokens.AccessToken = Token.Default;
        WriteToken(_tokenPath);

        Dictionary<string, string> requestParams = new(){
            { "client_id", _clientId },
            { "grant_type", "refresh_token" },
            { "refresh_token", _tokens.RefreshToken.Code }
        };
        HttpRequestMessage request = GeneratePostRequest("token", requestParams);

        DateTime accessTokenExpiry = DateTime.Now.Add(TimeSpan.FromSeconds(_accessTokenValiditySeconds));
        DateTime refreshTokenExpiry = DateTime.Now.Add(TimeSpan.FromDays(_refreshTokenValidityDays));

        HttpResponseMessage response = await _httpClient.SendAsync(request);
        _ = response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();
        RefreshResponse token = JsonSerializer.Deserialize<RefreshResponse>(responseBody) ?? throw new Exception();

        _tokens.RefreshToken.Expiry = refreshTokenExpiry;
        _tokens.AccessToken = new() { Code = token.AccessToken, Expiry = accessTokenExpiry };
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

        string responseBody = await response.Content.ReadAsStringAsync();
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