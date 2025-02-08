using System.Net.Http.Headers;
using EasonEetwViewer.Authentication.Abstractions;
using EasonEetwViewer.Authentication.Exceptions;

namespace EasonEetwViewer.Authentication.Services;
/// <summary>
/// Represents an authenticator without authentication.
/// </summary>
internal sealed class NullAuthenticator : IAuthenticator
{
    /// <inheritdoc/>
    /// <exception cref="NullAuthenticationException">This class does not support this operation.</exception>
    public Task<AuthenticationHeaderValue> GetAuthenticationHeader()
        => throw new NullAuthenticationException();
    /// <inheritdoc/>
    /// <exception cref="NullAuthenticationException">This class does not support this operation.</exception>
    public Task<AuthenticationHeaderValue> GetNewAuthenticationHeader()
        => throw new NullAuthenticationException();
    /// <inheritdoc/>
    public override string? ToString()
        => null;
    /// <summary>
    /// Creates a new instance of <see cref="NullAuthenticator"/>.
    /// </summary>
    private NullAuthenticator() { }
    /// <summary>
    /// The shared instance of <see cref="NullAuthenticator"/>.
    /// </summary>
    public static IAuthenticator Instance { get; } = new NullAuthenticator();
}
