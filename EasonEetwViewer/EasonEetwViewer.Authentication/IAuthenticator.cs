using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace EasonEetwViewer.Authentication;

/// <summary>
/// Describes the interface of an authenticator for API calls.
/// </summary>
public interface IAuthenticator
{
    /// <summary>
    /// Returns an <c>AuthenticationHeaderValue</c> to be used in a HTTP request.
    /// </summary>
    /// <returns>An <c>AuthenticationHeaderValue</c>.</returns>
    public Task<AuthenticationHeaderValue> GetAuthenticationHeader();
    /// <summary>
    /// Returns an <c>AuthenticationHeaderValue</c> to be used in a HTTP request, and is forced to return a refreshed token.
    /// </summary>
    /// <returns>An refreshed <c>AuthenticationHeaderValue</c>.</returns>
    public Task<AuthenticationHeaderValue> GetNewAuthenticationHeader();
    public string ToJsonString();
    public static abstract IAuthenticator FromJsonString(string jsonString);
}
