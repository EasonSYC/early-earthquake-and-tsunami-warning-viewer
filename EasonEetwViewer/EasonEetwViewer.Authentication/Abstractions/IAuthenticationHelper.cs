using System.Net.Http.Headers;
using EasonEetwViewer.Authentication.Services;

namespace EasonEetwViewer.Authentication.Abstractions;
/// <summary>
/// Wraps around an <see cref="IAuthenticator"/> to provide authentication functionalities.
/// </summary>
public interface IAuthenticationHelper
{
    /// <summary>
    /// The current authentication status.
    /// </summary>
    AuthenticationStatus AuthenticationStatus { get; }
    /// <summary>
    /// When the status of the authentication has changed.
    /// </summary>
    event EventHandler? AuthenticationStatusChanged;
    /// <summary>
    /// Gets an authenticator header value to be used for authentication.
    /// </summary>
    /// <returns>The authenticator header value to be used, or <see langword="null"/> if failed.</returns>
    Task<AuthenticationHeaderValue?> GetAuthenticationHeaderAsync();
    /// <summary>
    /// Acts when the authenticator is invalid.
    /// </summary>
    /// <returns>A <see cref="Task"/> object that represents the asynchronous operation.</returns>
    Task InvalidAuthenticatorAsync(string message);
    /// <summary>
    /// Set the authenticator to <see cref="ApiKeyAuthenticator"/> with the specified API Key.
    /// </summary>
    /// <param name="apiKey">The API Key.</param>
    /// <returns>A <see cref="Task"/> object that represents the asynchronous operation.</returns>
    Task SetApiKeyAsync(string apiKey);
    /// <summary>
    /// Set the authenticator to <see cref="OAuth2Authenticator"/> which requires input from the browser.
    /// </summary>
    /// <returns>A <see cref="Task"/> object that represents the asynchronous operation.</returns>
    Task SetOAuthAsync();
    /// <summary>
    /// Unset the authenticator to <see cref="NullAuthenticator"/>.
    /// </summary>
    /// <returns>A <see cref="Task"/> object that represents the asynchronous operation.</returns>
    Task UnsetAuthenticatorAsync();
}