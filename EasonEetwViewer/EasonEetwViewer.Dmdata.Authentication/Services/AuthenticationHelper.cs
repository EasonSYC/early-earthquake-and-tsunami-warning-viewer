using System.Net.Http.Headers;
using EasonEetwViewer.Dmdata.Authentication.Abstractions;
using EasonEetwViewer.Dmdata.Authentication.Events;
using EasonEetwViewer.Dmdata.Authentication.Exceptions;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Dmdata.Authentication.Services;
/// <summary>
/// Default implmentation of <see cref="IAuthenticationHelper"/>.
/// </summary>
internal sealed class AuthenticationHelper : IAuthenticationHelper
{
    /// <summary>
    /// The logger used for the current instance.
    /// </summary>
    private readonly ILogger<AuthenticationHelper> _logger;
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
    /// <inheritdoc/>
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
    /// Creates a new instance of <see cref="AuthenticationHelper"/> from a string.
    /// </summary>
    /// <param name="filePath">The file path to write to when the authentication status is updated.</param>
    /// <param name="str">The string to be used to create the instance from.</param>
    /// <param name="logger">The logger to be used for the instance</param>
    /// <param name="helperLogger">The logger to be used for the <see cref="OAuth2Helper"/>.</param>
    /// <param name="oAuthLogger">The logger to be used for the <see cref="OAuth2Authenticator"/>.</param>
    /// <param name="oAuth2Options">The options for OAuth 2.</param>
    /// <returns>The new instance of <see cref="AuthenticationHelper"/>.</returns>
    internal static AuthenticationHelper FromString(
        string filePath,
        string? str,
        ILogger<AuthenticationHelper> logger,
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

        AuthenticationHelper authenticationHelper = new(
            authenticator,
            logger,
            helperLogger,
            oAuthLogger,
            oAuth2Options);
        authenticationHelper.StatusChanged += (sender, e) =>
        {
            string? content = e.Authentication;
            try
            {
                File.WriteAllText(filePath, content);
                logger.SucceededToWriteToFile(filePath, content);
            }
            catch (Exception)
            {
                logger.FailedToWriteToFile(filePath, content);
            }
        };
        return authenticationHelper;
    }
    /// <inheritdoc/>
    public AuthenticationStatus AuthenticationStatus
        => _authenticator is NullAuthenticator
            ? AuthenticationStatus.Null
            : _authenticator is ApiKeyAuthenticator
                ? AuthenticationStatus.ApiKey
                : AuthenticationStatus.OAuth;
    /// <summary>
    /// Creates a new instance of <see cref="AuthenticationHelper"/> with the options provided..
    /// </summary>
    /// <param name="currentAuthenticator">The current <see cref="IAuthenticator"/> to be used.</param>
    /// <param name="logger">The logger to be used for the instance</param>
    /// <param name="helperLogger">The logger to be used for the <see cref="OAuth2Helper"/>.</param>
    /// <param name="oAuthLogger">The logger to be used for the <see cref="OAuth2Authenticator"/>.</param>
    /// <param name="oAuth2Options">The options for OAuth 2.</param>
    private AuthenticationHelper(
        IAuthenticator currentAuthenticator,
        ILogger<AuthenticationHelper> logger,
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
    /// <inheritdoc/>
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

        StatusChanged?.Invoke(this, new()
        {
            AuthenticationStatus = AuthenticationStatus,
            Authentication = ToString()
        });
    }
    /// <inheritdoc/>
    public async Task SetApiKeyAsync(string apiKey)
    {
        await UnsetAuthenticatorAsync();
        _authenticator = new ApiKeyAuthenticator(apiKey);
        _logger.ChangedToApiKey();
        StatusChanged?.Invoke(this, new()
        {
            AuthenticationStatus = AuthenticationStatus,
            Authentication = ToString()
        });
    }
    /// <inheritdoc/>
    public async Task InvalidAuthenticatorAsync(string message)
    {
        _logger.InvalidAuthentication(message);
        await UnsetAuthenticatorAsync();
    }
    /// <inheritdoc/>
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
        StatusChanged?.Invoke(this, new()
        {
            AuthenticationStatus = AuthenticationStatus,
            Authentication = ToString()
        });
    }
    /// <inheritdoc/>
    public event EventHandler<AuthenticationStatusChangedEventArgs>? StatusChanged;
}