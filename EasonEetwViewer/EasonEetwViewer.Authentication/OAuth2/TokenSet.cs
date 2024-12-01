using System.Text.Json.Serialization;

namespace EasonEetwViewer.Authentication.OAuth2;

internal class TokenSet
{
    static internal TokenSet Default = new()
    {
        Port = 0,
        CodeVerifier = string.Empty,
        AccessToken = Token.Default,
        RefreshToken = Token.Default
    };
    [JsonPropertyName("port")]
    public required int Port { get; set; } = -1;
    [JsonPropertyName("code_verifier")]
    public required string CodeVerifier { get; set; } = string.Empty;
    [JsonPropertyName("access_token")]
    public required Token AccessToken { get; set; } = Token.Default;
    [JsonPropertyName("grant_code")]
    public required Token RefreshToken { get; set; } = Token.Default;
}
