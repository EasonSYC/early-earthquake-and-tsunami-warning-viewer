using System.Text.Json.Serialization;

namespace EasonEetwViewer.Data;

internal record OAuthToken
{
    static internal OAuthToken Default = new(){
        Port = 0,
        CodeVerifier = string.Empty,
        AccessToken = Token.Default,
        RefreshToken = Token.Default
    };
    [JsonPropertyName("port")]
    internal required int Port { get; set; }
    [JsonPropertyName("code_verifier")]
    internal required string CodeVerifier { get; set; }
    [JsonPropertyName("access_token")]
    internal required Token AccessToken { get; set; }
    [JsonPropertyName("grant_code")]
    internal required Token RefreshToken { get; set; }
}
