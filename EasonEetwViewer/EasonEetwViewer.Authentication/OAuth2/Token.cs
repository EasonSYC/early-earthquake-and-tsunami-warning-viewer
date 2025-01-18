using System.Text.Json.Serialization;

namespace EasonEetwViewer.Authentication.OAuth2;

/// <summary>
/// Represents a token for OAuth 2.
/// </summary>
internal class Token
{
    /// <summary>
    /// The token code.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("code")]
    internal string Code { get; set; }

    /// <summary>
    /// The expiry time of the token.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("expiry")]
    internal DateTimeOffset Expiry { get; private set; }

    /// <summary>
    /// The validity timespan of the token.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("validity")]
    internal TimeSpan Validity { get; private init; }

    /// <summary>
    /// The validity of the token.
    /// </summary>
    internal bool IsValid => Expiry.CompareTo(DateTimeOffset.Now) > 0;

    /// <summary>
    /// Resets the validity of the token by calculating the new expiry time from now.
    /// </summary>
    internal void ResetValidity() => Expiry = DateTimeOffset.Now + Validity;

    /// <summary>
    /// Resets the token code and the expiry time.
    /// </summary>
    internal void Reset()
    {
        Code = string.Empty;
        Expiry = new();
    }

    /// <summary>
    /// Creates a new instance of the token only specifying the validity time span.
    /// </summary>
    /// <param name="validity">The validity timespan of the token.</param>
    internal Token(TimeSpan validity)
    {
        Code = string.Empty;
        Expiry = new();
        Validity = validity;
    }

    /// <summary>
    /// Creates a new instance of the token specifying the code, expiry and validity.
    /// </summary>
    /// <param name="code">The token code.</param>
    /// <param name="expiry">The expiry time of the token.</param>
    /// <param name="validity">The validity timespan of the token.</param>
    [JsonConstructor]
    internal Token(string code, DateTimeOffset expiry, TimeSpan validity)
    {
        Code = code;
        Expiry = expiry;
        Validity = validity;
    }
}
