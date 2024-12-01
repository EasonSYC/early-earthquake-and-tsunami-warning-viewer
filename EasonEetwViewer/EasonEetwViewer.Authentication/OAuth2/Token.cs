using System.Text.Json.Serialization;

namespace EasonEetwViewer.Authentication.OAuth2;

internal class Token
{
    static internal Token Default = new()
    {
        Code = string.Empty,
        Expiry = new(0)
    };
    [JsonPropertyName("code")]
    public required string Code { get; set; } = string.Empty;
    [JsonPropertyName("expiry")]
    public required DateTime Expiry { get; set; } =new(0);
    internal bool IsValid => Expiry.CompareTo(DateTime.Now) > 0;
}
