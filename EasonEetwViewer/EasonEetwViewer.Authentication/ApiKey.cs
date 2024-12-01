using System.Text;
using System.Net.Http.Headers;

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
    /// <param name="apiKey">The <c>apiKey</c> to be used.</param>
    /// <exception cref="ArgumentException">When <c>apiKey</c> is not valid.</exception>
    public ApiKey(string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey) || apiKey.Length <= 4 || apiKey.Substring(0, 4) != "AKe.")
        {
            throw new ArgumentException($"{apiKey} is not a valid API Key.", nameof(apiKey));
        }

        byte[] plainTextBytes = Encoding.UTF8.GetBytes($"{apiKey}:");
        string val = Convert.ToBase64String(plainTextBytes);
        _header = new("Basic", val);
    }
}