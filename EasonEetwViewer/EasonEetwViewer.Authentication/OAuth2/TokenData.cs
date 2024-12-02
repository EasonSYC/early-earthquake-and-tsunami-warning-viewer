using System.Text.Json.Serialization;

namespace EasonEetwViewer.Authentication.OAuth2;

/// <summary>
/// Represents a set of credentials used by OAuth 2 in the program.
/// </summary>
internal class TokenData
{
    /// <summary>
    /// The port number used when listening to the grant code.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("port")]
    internal int Port { get; set; }
    /// <summary>
    /// The code verifier communicated between client and server for code challenge.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("codeVerifier")]
    internal string CodeVerifier { get; set; }
    /// <summary>
    /// The scopes of the tokens, in the form of a hash set.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("scopes")]
    internal HashSet<string> Scopes { get; private init; }
    /// <summary>
    /// The access token.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("accessToken")]
    internal Token AccessToken { get; private init; }
    /// <summary>
    /// The refresh token.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("refreshToken")]
    internal Token RefreshToken { get; private init; }
    /// <summary>
    /// The scopes of the tokens, in the form of a string joint with a space separating.
    /// </summary>
    internal string ScopeString => string.Join(' ', Scopes);
    /// <summary>
    /// Resets the valididity for both the access token and the refresh token.
    /// </summary>
    internal void ResetValidity()
    {
        AccessToken.ResetValidity();
        RefreshToken.ResetValidity();
    }
    /// <summary>
    /// Creates a new instance of token data only specifying the scope, the access token validity timespan and the refresh token validity timespan.
    /// </summary>
    /// <param name="scopes">The scopes of the tokens, in the form of a hash set.</param>
    /// <param name="accessTokenValidity">The validity of the access token.</param>
    /// <param name="refreshTokenValidity">The validity of the refresh token.</param>
    internal TokenData(HashSet<string> scopes, TimeSpan accessTokenValidity, TimeSpan refreshTokenValidity)
    {
        Port = 0;
        CodeVerifier = string.Empty;
        Scopes = scopes;
        AccessToken = new(accessTokenValidity);
        RefreshToken = new(refreshTokenValidity);
    }
    /// <summary>
    /// Creates a new instance of token data specifying the scope, the port, the code verifier, the access token and the refresh token.
    /// </summary>
    /// <param name="port">The port number used when listening to the grant code.</param>
    /// <param name="codeVerifier">The code verifier communicated between client and server for code challenge.</param>
    /// <param name="scopes">The scopes of the tokens, in the form of a hash set.</param>
    /// <param name="accessToken">The access token.</param>
    /// <param name="refreshToken">The refresh token.</param>
    [JsonConstructor]
    internal TokenData(int port, string codeVerifier, HashSet<string> scopes, Token accessToken, Token refreshToken)
    {
        Port = port;
        CodeVerifier = codeVerifier;
        Scopes = scopes;
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}
