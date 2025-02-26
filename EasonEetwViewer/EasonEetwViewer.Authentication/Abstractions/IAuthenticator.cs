using System.Net.Http.Headers;

namespace EasonEetwViewer.Authentication.Abstractions;

/// <summary>
/// Describes the interface of an authenticator for API calls.
/// </summary>
public interface IAuthenticator
{
    /// <summary>
    /// Returns an <see cref="AuthenticationHeaderValue"/> to be used in a HTTP request.
    /// </summary>
    /// <returns>An <see cref="AuthenticationHeaderValue"/>.</returns>
    Task<AuthenticationHeaderValue> GetAuthenticationHeader();
    /// <summary>
    /// Returns an <see cref="AuthenticationHeaderValue"/> to be used in a HTTP request, and is forced to return a refreshed token.
    /// </summary>
    /// <returns>An refreshed <see cref="AuthenticationHeaderValue"/>.</returns>
    Task<AuthenticationHeaderValue> GetNewAuthenticationHeader();
}
