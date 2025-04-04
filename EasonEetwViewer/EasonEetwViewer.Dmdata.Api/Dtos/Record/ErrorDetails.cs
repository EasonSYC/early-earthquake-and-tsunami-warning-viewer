using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Api.Dtos.Record;

/// <summary>
/// Represents an error thrown by the API call.
/// </summary>
public record ErrorDetails
{
    /// <summary>
    /// The property <c>message</c>. The message of the error.
    /// </summary>
    [JsonPropertyName("message")]
    public required string Message { get; init; }
    /// <summary>
    /// The property <c>code</c>. The HTTP error status code.
    /// </summary>
    [JsonPropertyName("code")]
    public required int Code { get; init; }
}