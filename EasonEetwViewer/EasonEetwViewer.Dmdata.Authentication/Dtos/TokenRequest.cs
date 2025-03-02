using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Authentication.Dtos;

/// <summary>
/// Represents a reponse from OAuth2 server to request a pair of token.
/// </summary>
internal sealed record TokenRequest : TokenRefresh
{
    /// <summary>
    /// The property <c>refresh_token</c>, the refresh token acquired.
    /// </summary>
    [JsonPropertyName("refresh_token")]
    public required string RefreshToken { get; init; }
}
