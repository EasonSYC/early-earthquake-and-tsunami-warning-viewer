using System;
using System.Text.Json.Serialization;

namespace EasonEetwViewer.Authentication.OAuth2;

public record RefreshResponse
{
    [JsonPropertyName("access_token")]
    public required string AccessToken { get; init; }

    [JsonPropertyName("token_type")]
    public string TokenType { get; } = "Bearer";

    [JsonPropertyName("expires_in")]
    public int Expiry { get; } = 21600;

    [JsonPropertyName("scope")]
    public required string Scope { get; init; }
}
