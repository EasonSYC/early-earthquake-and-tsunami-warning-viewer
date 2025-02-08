using System.Text.Json.Serialization;

namespace EasonEetwViewer.Authentication.OAuth2;
/// <summary>
/// Represents an error in the response from an OAuth HTTP Request.
/// </summary>
internal record Error
{
    /// <summary>
    /// The property <c>error</c>, the error message.
    /// </summary>
    [JsonPropertyName("error")]
    public required string Short { get; init; }
    /// <summary>
    /// The property <c>error_description</c>, the error description.
    /// </summary>
    [JsonPropertyName("error_description")]
    public required string Description { get; init; }
}