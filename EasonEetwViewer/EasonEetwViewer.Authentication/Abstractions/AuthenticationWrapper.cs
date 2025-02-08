using EasonEetwViewer.Authentication.Services;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Authentication.Abstractions;
/// <summary>
/// Wraps around an <see cref="IAuthenticator"/> to provide authentication functionalities.
/// </summary>
public sealed class AuthenticationWrapper
{
    /// <summary>
    /// The logger used for the current instance.
    /// </summary>
    private readonly ILogger<AuthenticationWrapper> _logger;
    /// <summary>
    /// The logger used for the <see cref="OAuth2Authenticator"/>.
    /// </summary>
    private readonly ILogger<OAuth2Authenticator> _oAuthLogger;
    /// <summary>
    /// The logger used for the <see cref="OAuth2Helper"/>.
    /// </summary>
    private readonly ILogger<OAuth2Helper> _helperLogger;
    /// <summary>
    /// The options for OAuth 2.
    /// </summary>
    private readonly OAuth2Options _oAuth2Options;
    /// <summary>
    /// The <see cref="IAuthenticator"/> to be used for the authentication.
    /// </summary>
    public IAuthenticator Authenticator { get; private set; }
    /// <inheritdoc/>
    public override string? ToString()
        => Authenticator.ToString();
    /// <summary>
    /// Creates a new instance of <see cref="AuthenticationWrapper"/> from a string.
    /// </summary>
    /// <param name="str">The string to be used to create the instance from.</param>
    /// <param name="logger">The logger to be used for the instance</param>
    /// <param name="helperLogger">The logger to be used for the <see cref="OAuth2Helper"/>.</param>
    /// <param name="oAuthLogger">The logger to be used for the <see cref="OAuth2Authenticator"/>.</param>
    /// <param name="oAuth2Options">The options for OAuth 2.</param>
    /// <returns>The new instance of <see cref="AuthenticationWrapper"/>.</returns>
    internal static AuthenticationWrapper FromString(
        string? str,
        ILogger<AuthenticationWrapper> logger,
        ILogger<OAuth2Helper> helperLogger,
        ILogger<OAuth2Authenticator> oAuthLogger,
        OAuth2Options oAuth2Options)
    {
        IAuthenticator authenticator
            = str is null
                ? NullAuthenticator.Instance
                : str.StartsWith("AKe.")
                    ? new ApiKeyAuthenticator(str)
                    : str.StartsWith("ARh.")
                        ? OAuth2Provides.GetOAuth2Authenticator(oAuth2Options, str, oAuthLogger)
                        : NullAuthenticator.Instance;
        return new AuthenticationWrapper(
            authenticator,
            logger,
            helperLogger,
            oAuthLogger,
            oAuth2Options);
    }
    /// <summary>
    /// The current authentication status.
    /// </summary>
    public AuthenticationStatus AuthenticationStatus
        => Authenticator is NullAuthenticator
            ? AuthenticationStatus.Null
            : Authenticator is ApiKeyAuthenticator
                ? AuthenticationStatus.ApiKey
                : AuthenticationStatus.OAuth;
    /// <summary>
    /// Creates a new instance of <see cref="AuthenticationWrapper"/> with the options provided..
    /// </summary>
    /// <param name="currentAuthenticator">The current <see cref="IAuthenticator"/> to be used.</param>
    /// <param name="logger">The logger to be used for the instance</param>
    /// <param name="helperLogger">The logger to be used for the <see cref="OAuth2Helper"/>.</param>
    /// <param name="oAuthLogger">The logger to be used for the <see cref="OAuth2Authenticator"/>.</param>
    /// <param name="oAuth2Options">The options for OAuth 2.</param>
    private AuthenticationWrapper(
        IAuthenticator currentAuthenticator,
        ILogger<AuthenticationWrapper> logger,
        ILogger<OAuth2Helper> helperLogger,
        ILogger<OAuth2Authenticator> oAuthLogger,
        OAuth2Options oAuth2Options)
    {
        Authenticator = currentAuthenticator;
        _logger = logger;
        _helperLogger = helperLogger;
        _oAuthLogger = oAuthLogger;
        _oAuth2Options = oAuth2Options;
    }
    /// <summary>
    /// Set the authenticator to <see cref="OAuth2Authenticator"/> which requires input from the browser.
    /// </summary>
    /// <returns>A <see cref="Task"/> object that represents the asynchronous operation.</returns>
    public async Task SetOAuthAsync()
    {
        await UnsetAuthenticatorAsync();
        Authenticator = await OAuth2Provides.GetOAuth2Authenticator(_oAuth2Options, _helperLogger, _oAuthLogger);
        AuthenticationStatusChanged?.Invoke(this, EventArgs.Empty);
    }
    /// <summary>
    /// Set the authenticator to <see cref="ApiKeyAuthenticator"/> with the specified API Key.
    /// </summary>
    /// <param name="apiKey">The API Key.</param>
    /// <returns>A <see cref="Task"/> object that represents the asynchronous operation.</returns>
    public async Task SetApiKeyAsync(string apiKey)
    {
        await UnsetAuthenticatorAsync();
        Authenticator = new ApiKeyAuthenticator(apiKey);
        AuthenticationStatusChanged?.Invoke(this, EventArgs.Empty);
    }
    /// <summary>
    /// Unset the authenticator to <see cref="NullAuthenticator"/>.
    /// </summary>
    /// <returns>A <see cref="Task"/> object that represents the asynchronous operation.</returns>
    public async Task UnsetAuthenticatorAsync()
    {
        if (Authenticator is OAuth2Authenticator oAuth)
        {
            await oAuth.RevokeTokens();
        }

        Authenticator = NullAuthenticator.Instance;
        AuthenticationStatusChanged?.Invoke(this, EventArgs.Empty);
    }
    /// <summary>
    /// When the status of the authentication has changed.
    /// </summary>
    public event EventHandler? AuthenticationStatusChanged;
}
