using System.Text.Json.Serialization;

namespace EasonEetwViewer.Authentication.OAuth2.Dto;

internal record TokenRefresh
{
    [JsonInclude]
    [JsonPropertyName("access_token")]
    internal required string AccessToken { get; init; }
    [JsonInclude]
    [JsonPropertyName("token_type")]
    internal string TokenType { get; } = "Bearer";
    [JsonInclude]
    [JsonPropertyName("expires_in")]
    internal int Expiry { get; } = 21600;
    [JsonInclude]
    [JsonPropertyName("scope")]
    internal required string Scope { get; init; }
}
