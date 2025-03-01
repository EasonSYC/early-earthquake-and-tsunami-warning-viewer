using System.Net.Http.Headers;
using EasonEetwViewer.Authentication.Exceptions;
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
    private IAuthenticator _authenticator;
    /// <summary>
    /// Gets an authenticator header value to be used for authentication.
    /// </summary>
    /// <returns>The authenticator header value to be used, or <see langword="null"/> if failed.</returns>
    public async Task<AuthenticationHeaderValue?> GetAuthenticationHeaderAsync()
    {
        if (AuthenticationStatus is AuthenticationStatus.Null)
        {
            return null;
        }
        else
        {
            try
            {
                return await _authenticator.GetAuthenticationHeaderAsync();
            }
            catch (OAuthException ex)
            {
                _logger.OAuthExceptionIgnored(ex.ToString());
                await UnsetAuthenticatorAsync();
                return null;
            }
        }
    }

    /// <inheritdoc/>
    public override string? ToString()
        => _authenticator.ToString();
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
        IAuthenticator authenticator;
        if (str is null)
        {
            authenticator = NullAuthenticator.Instance;
        }
        else if (str.StartsWith("AKe."))
        {
            authenticator = new ApiKeyAuthenticator(str);
        }
        else if (str.StartsWith("ARh."))
        {
            try
            {
                authenticator = OAuth2Provider.GetOAuth2Authenticator(oAuth2Options, str, oAuthLogger);
            }
            catch (OAuthException ex)
            {
                logger.OAuthExceptionIgnored(ex.ToString());
                authenticator = NullAuthenticator.Instance;
            }
            catch (Exception ex)
            {
                logger.OtherExceptionIgnored(ex.ToString());
                authenticator = NullAuthenticator.Instance;
            }
        }
        else
        {
            authenticator = NullAuthenticator.Instance;
        }

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
        => _authenticator is NullAuthenticator
            ? AuthenticationStatus.Null
            : _authenticator is ApiKeyAuthenticator
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
        _authenticator = currentAuthenticator;
        _logger = logger;
        _helperLogger = helperLogger;
        _oAuthLogger = oAuthLogger;
        _oAuth2Options = oAuth2Options;
        _logger.Instantiated();
    }
    /// <summary>
    /// Set the authenticator to <see cref="OAuth2Authenticator"/> which requires input from the browser.
    /// </summary>
    /// <returns>A <see cref="Task"/> object that represents the asynchronous operation.</returns>
    public async Task SetOAuthAsync()
    {
        await UnsetAuthenticatorAsync();
        try
        {
            _authenticator = await OAuth2Provider.GetOAuth2Authenticator(_oAuth2Options, _helperLogger, _oAuthLogger);
        }
        catch (OAuthException ex)
        {
            _logger.OAuthExceptionIgnored(ex.ToString());
        }
        catch (Exception ex)
        {
            _logger.OtherExceptionIgnored(ex.ToString());
        }

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
        _authenticator = new ApiKeyAuthenticator(apiKey);
        _logger.ChangedToApiKey();
        AuthenticationStatusChanged?.Invoke(this, EventArgs.Empty);
    }
    /// <summary>
    /// Acts when the authenticator is invalid.
    /// </summary>
    /// <returns>A <see cref="Task"/> object that represents the asynchronous operation.</returns>
    public async Task InvalidAuthenticatorAsync(string message)
    {
        _logger.InvalidAuthentication(message);
        await UnsetAuthenticatorAsync();
    }
    /// <summary>
    /// Unset the authenticator to <see cref="NullAuthenticator"/>.
    /// </summary>
    /// <returns>A <see cref="Task"/> object that represents the asynchronous operation.</returns>
    public async Task UnsetAuthenticatorAsync()
    {
        _logger.Unsetting();
        if (_authenticator is OAuth2Authenticator oAuth)
        {
            try
            {
                _logger.RevokingOAuth2Token();
                await oAuth.RevokeTokens();
            }
            catch (OAuthException ex)
            {
                _logger.OAuthExceptionIgnored(ex.ToString());
            }
            catch (Exception ex)
            {
                _logger.OtherExceptionIgnored(ex.ToString());
            }
        }

        _authenticator = NullAuthenticator.Instance;
        _logger.Unset();
        AuthenticationStatusChanged?.Invoke(this, EventArgs.Empty);
    }
    /// <summary>
    /// When the status of the authentication has changed.
    /// </summary>
    public event EventHandler? AuthenticationStatusChanged;
}