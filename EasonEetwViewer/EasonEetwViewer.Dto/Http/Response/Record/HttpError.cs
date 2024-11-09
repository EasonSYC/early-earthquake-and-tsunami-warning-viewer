using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.Http.Response.Records;

/// <summary>
/// Represents an error thrown by the API call.
/// </summary>
public record HttpError
{
    /// <summary>
    /// The property <c>message</c>. The message of the error.
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; init; } = string.Empty;
    /// <summary>
    /// The property <c>code</c>. The HTTP error status code.
    /// </summary>
    [JsonPropertyName("code")]
    public int Code { get; init; }
}