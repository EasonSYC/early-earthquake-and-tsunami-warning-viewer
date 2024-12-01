using System;
using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Web;
using System.Net;
using System.Net.Sockets;
using EasonEetwViewer.Dto.OAuth;
using System.Diagnostics;

namespace EasonEetwViewer.Data;

public class Authentication
{
    private readonly string _clientId;
    private readonly string _host;
    private readonly Uri _base;
    private readonly HttpClient _httpClient;
    private readonly OAuthToken _tokens;
    private readonly string _scopes;
    private const string _allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    private const string _redirectPath = "eeetwv-code-auth/";
    private const int _codeLength = 64;
    private const int _stateLength = 8;
    private const int _accessTokenValiditySeconds = 21600;
    private const int _refreshTokenValidityDays = 183;

    public Authentication(string clientId, string basePath, string host, string scopes)
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
        Console.WriteLine(_httpClient.BaseAddress.ToString());
        _scopes = scopes;
        _tokens = ReadToken("credentials.json");
    }
    private static OAuthToken ReadToken(string filePath)
    {
        if (File.Exists(filePath))
        {
            string fileBody = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<OAuthToken>(fileBody) ?? OAuthToken.Default;
        }

        return OAuthToken.Default;
    }
    private static string VerifierToChallenge(string verifier)
    {
        byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(verifier));
        string challenge = Convert.ToBase64String(hash).Replace("+", "-").Replace("/", "_").TrimEnd('=');
        return challenge;
    }
    public async Task WriteTokenAsync(string filePath)
    {
        string fileBody = JsonSerializer.Serialize(_tokens);
        await File.WriteAllTextAsync(filePath, fileBody);
    }
    public async Task<string> GetAccessTokenAsync()
    {
        if (_tokens.AccessToken.IsValid)
        {
            return _tokens.AccessToken.Code;
        }
        if (_tokens.RefreshToken.IsValid)
        {
            await RenewAccessTokenAsync();
            return _tokens.AccessToken.Code;
        }
        await RenewRefreshTokenAsync();
        return _tokens.AccessToken.Code;
    }
    public async Task<string> ForceNewAccessTokenAsync()
    {
        Task revokeAccessToken = RevokeTokenAsync(_tokens.AccessToken.Code);
        _tokens.AccessToken = Token.Default;
        Task<string> getNewAccessToken = GetAccessTokenAsync();
        await Task.WhenAll(revokeAccessToken, getNewAccessToken);
        return getNewAccessToken.Result;
    }

    private void FindUnusedPort()
    {
        TcpListener listener = new(IPAddress.Loopback, 0);
        listener.Start();
        int port = ((IPEndPoint)listener.LocalEndpoint).Port;
        listener.Stop();

        Console.WriteLine(port);

        _tokens.Port = port;
    }

    private async Task<string> GetGrantCode()
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

        Console.WriteLine(grantCode);

        return requestState != state ? throw new Exception() : grantCode;
    }

    private async Task RenewRefreshTokenAsync()
    {
        Task revokeAccessToken = RevokeTokenAsync(_tokens.AccessToken.Code);
        Task revokeRefreshToken = RevokeTokenAsync(_tokens.RefreshToken.Code);
        _tokens.AccessToken = Token.Default;
        _tokens.RefreshToken = Token.Default;

        string grantCode = await GetGrantCode();

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
        HttpRequestMessage request = GenerateRequest("token", requestParams);

        DateTime accessTokenExpiry = DateTime.Now.Add(TimeSpan.FromSeconds(_accessTokenValiditySeconds));
        DateTime refreshTokenExpiry = DateTime.Now.Add(TimeSpan.FromDays(_refreshTokenValidityDays));

        HttpResponseMessage response = await _httpClient.SendAsync(request);
        _ = response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();
        AuthToken token = JsonSerializer.Deserialize<AuthToken>(responseBody) ?? throw new Exception();

        _tokens.RefreshToken = new() { Code = token.RefreshToken, Expiry = refreshTokenExpiry };
        _tokens.AccessToken = new() { Code = token.AccessToken, Expiry = accessTokenExpiry };


        Console.WriteLine(token.RefreshToken);
        Console.WriteLine(token.AccessToken);

        await Task.WhenAll(revokeAccessToken, revokeRefreshToken);
    }
    private async Task RenewAccessTokenAsync()
    {
        Dictionary<string, string> requestParams = new(){
            { "client_id", _clientId },
            { "grant_type", "refresh_token" },
            { "refresh_token", _tokens.RefreshToken.Code }
        };
        HttpRequestMessage request = GenerateRequest("token", requestParams);

        DateTime accessTokenExpiry = DateTime.Now.Add(TimeSpan.FromSeconds(_accessTokenValiditySeconds));
        DateTime refreshTokenExpiry = DateTime.Now.Add(TimeSpan.FromDays(_refreshTokenValidityDays));

        HttpResponseMessage response = await _httpClient.SendAsync(request);
        _ = response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();
        AuthRefresh token = JsonSerializer.Deserialize<AuthRefresh>(responseBody) ?? throw new Exception();

        Console.WriteLine(token.AccessToken);

        _tokens.RefreshToken.Expiry = refreshTokenExpiry;
        _tokens.AccessToken = new() { Code = token.AccessToken, Expiry = accessTokenExpiry };
    }
    public async Task Revoke()
    {
        Task revokeAccessToken = RevokeTokenAsync(_tokens.AccessToken.Code);
        Task revokeRefreshToken = RevokeTokenAsync(_tokens.RefreshToken.Code);

        _tokens.AccessToken = Token.Default;
        _tokens.RefreshToken = Token.Default;

        await Task.WhenAll(revokeAccessToken, revokeRefreshToken);
    }
    private async Task RevokeTokenAsync(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return;
        }
        Dictionary<string, string> requestParams = new(){
            { "client_id", _clientId },
            { "token", token }
        };
        HttpRequestMessage request = GenerateRequest("revoke", requestParams);
        HttpResponseMessage response = await _httpClient.SendAsync(request);
        _ = response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();
    }
    private HttpRequestMessage GenerateRequest(string requestUri, Dictionary<string, string> requestParams)
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
