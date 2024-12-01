using System;
using System.Text.Json.Serialization;

namespace EasonEetwViewer.Data;

internal record Token
{
    static internal Token Default = new()
    {
        Code = string.Empty,
        Expiry = new()
    };
    [JsonPropertyName("code")]
    internal required string Code { get; set; }
    [JsonPropertyName("expiry")]
    internal required DateTime Expiry { get; set; }

    internal bool IsValid => Expiry.CompareTo(DateTime.Now) > 0;
}
