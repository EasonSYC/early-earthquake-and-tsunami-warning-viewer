using System.Net.Http.Headers;
using System.Text;

namespace EasonEetwViewer.Authentication;

/// <summary>
/// An implementation of <c>IAuthenticator</c>, using API Keys to authenticate.
/// </summary>
public class ApiKey : IAuthenticator
{
    /// <summary>
    /// The header for HTTP request.
    /// </summary>
    private readonly AuthenticationHeaderValue _header;
    /// <summary>
    /// Returns an <c>AuthenticationHeaderValue</c> to be used in a HTTP request.
    /// This behaviour is identical to <c>GetNewAuthenticationHeader</c> since it is impossible to refresh an API Key.
    /// </summary>
    /// <returns>An <c>AuthenticationHeaderValue</c> using <c>Basic</c>.</returns>
    public Task<AuthenticationHeaderValue> GetAuthenticationHeader() => Task.FromResult(_header);

    /// <summary>
    /// Returns an <c>AuthenticationHeaderValue</c> to be used in a HTTP request.
    /// This behaviour is identical to <c>GetAuthenticationHeader</c> since it is impossible to refresh an API Key.
    /// </summary>
    /// <returns>An <c>AuthenticationHeaderValue</c> using <c>Basic</c>.</returns>
    public Task<AuthenticationHeaderValue> GetNewAuthenticationHeader() => Task.FromResult(_header);

    /// <summary>
    /// Creates a new instance of the class with a given API Key.
    /// </summary>
    /// <param name="apiKey">The API Key to be used.</param>
    public ApiKey(string apiKey)
    {
        byte[] plainTextBytes = Encoding.UTF8.GetBytes($"{apiKey}:");
        string val = Convert.ToBase64String(plainTextBytes);
        _header = new("Basic", val);
    }
}