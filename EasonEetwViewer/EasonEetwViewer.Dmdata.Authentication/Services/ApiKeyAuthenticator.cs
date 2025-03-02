using System.Net.Http.Headers;
using System.Text;
using EasonEetwViewer.Dmdata.Authentication.Abstractions;

namespace EasonEetwViewer.Dmdata.Authentication.Services;

/// <summary>
/// Implementation of <see cref="IAuthenticator"/>, using API Keys to authenticate.
/// </summary>
internal sealed class ApiKeyAuthenticator : IAuthenticator
{
    /// <summary>
    /// The header for HTTP request.
    /// </summary>
    private readonly AuthenticationHeaderValue _header;
    /// <summary>
    /// The API Key.
    /// </summary>
    private readonly string _apiKey;
    /// <inheritdoc/>
    public Task<AuthenticationHeaderValue> GetAuthenticationHeaderAsync() => Task.FromResult(_header);
    /// <inheritdoc/>
    public override string ToString()
        => _apiKey;
    /// <summary>
    /// Creates a new instance of the class with a given API Key.
    /// </summary>
    /// <param name="apiKey">The API Key to be used.</param>
    public ApiKeyAuthenticator(string apiKey)
    {
        if (!apiKey.StartsWith("AKe."))
        {
            throw new ArgumentException($"API Key does not have valid format: {apiKey}", nameof(apiKey));
        }

        _apiKey = apiKey;
        byte[] plainTextBytes = Encoding.UTF8.GetBytes($"{apiKey}:");
        string val = Convert.ToBase64String(plainTextBytes);
        _header = new("Basic", val);
    }
}