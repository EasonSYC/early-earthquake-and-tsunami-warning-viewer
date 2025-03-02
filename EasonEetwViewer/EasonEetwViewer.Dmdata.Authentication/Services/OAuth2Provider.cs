using EasonEetwViewer.Dmdata.Authentication.Abstractions;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Dmdata.Authentication.Services;
/// <summary>
/// Provides <see cref="OAuth2Authenticator"/> as <see cref="IAuthenticator"/>.
/// </summary>
internal static class OAuth2Provider
{
    /// <summary>
    /// Creates a new instance of <see cref="OAuth2Authenticator"/> by requesting a new refresh token and a new access token.
    /// </summary>
    /// <param name="options">The options to be used for OAuth.</param>
    /// <param name="helperLogger">The logger for the OAuth helper.</param>
    /// <param name="logger">The logger for the OAuth authenticator.</param>
    /// <returns>An asynchronous task whose result is the authenticator desired.</returns>
    public static async Task<IAuthenticator> GetOAuth2Authenticator(OAuth2Options options, ILogger<OAuth2Helper> helperLogger, ILogger<OAuth2Authenticator> logger)
    {
        (string refreshToken, string accessToken) = await new OAuth2Helper(
            options.ClientId,
            string.Join(' ', options.Scopes),
            options.BaseUri,
            options.Host,
            options.RedirectPath,
            options.WebPageString,
            helperLogger)
            .GetRefreshTokenAsync();
        return new OAuth2Authenticator(
            options.ClientId,
            options.BaseUri,
            options.Host,
            refreshToken,
            accessToken,
            logger);
    }
    /// <summary>
    /// Creates a new instance of <see cref="OAuth2Authenticator"/> using the existing refresh token.
    /// </summary>
    /// <param name="options">The options to be used for OAuth.</param>
    /// <param name="refreshToken">The refresh token to be used.</param>
    /// <param name="logger">The looger for the OAuth authenticator.</param>
    /// <returns>The authenticator desired.</returns>
    public static IAuthenticator GetOAuth2Authenticator(OAuth2Options options, string refreshToken, ILogger<OAuth2Authenticator> logger)
        => new OAuth2Authenticator(
            options.ClientId,
            options.BaseUri,
            options.Host,
            refreshToken,
            null,
            logger);
}
