using System.Text.Json.Serialization;

namespace EasonEetwViewer.Authentication.OAuth2;

/// <summary>
/// Represents a reponse from OAuth2 server to refresh a token.
/// </summary>
internal record TokenRefresh
{
    /// <summary>
    /// The property <c>access_token</c>, the new access token acquired.
    /// </summary>
    [JsonPropertyName("access_token")]
    public required string AccessToken { get; init; }
    /// <summary>
    /// The property <c>token_type</c>, the type of token. A constant <c>Bearer</c>.
    /// </summary>
    [JsonPropertyName("token_type")]
    public string TokenType { get; } = "Bearer";
    /// <summary>
    /// The property <c>expires_in</c>, the validity of the token. A constant <c>21600</c>.
    /// </summary>
    [JsonPropertyName("expires_in")]
    public int Expiry { get; } = 21600;
    /// <summary>
    /// The property <c>scope</c>, the scopes that the token is valid in.
    /// </summary>
    [JsonPropertyName("scope")]
    public required string Scope { get; init; }
}
