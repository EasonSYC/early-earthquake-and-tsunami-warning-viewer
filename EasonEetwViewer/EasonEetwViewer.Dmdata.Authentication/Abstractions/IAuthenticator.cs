using System.Net.Http.Headers;

namespace EasonEetwViewer.Dmdata.Authentication.Abstractions;

/// <summary>
/// Describes the interface of an authenticator for API calls.
/// </summary>
internal interface IAuthenticator
{
    /// <summary>
    /// Returns an <see cref="AuthenticationHeaderValue"/> to be used in a HTTP request.
    /// </summary>
    /// <returns>An <see cref="AuthenticationHeaderValue"/>.</returns>
    Task<AuthenticationHeaderValue> GetAuthenticationHeaderAsync();
}
