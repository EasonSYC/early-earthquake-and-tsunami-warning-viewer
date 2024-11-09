using System.Text.Json.Serialization;
using EasonEetwViewer.Dto.Http.Response.Enum;
using EasonEetwViewer.Dto.Http.Response.Model;

namespace EasonEetwViewer.Dto.Http.Response.Record;

/// <summary>
/// Represents an Error HTTP response.
/// </summary>
public record ErrorResponse : ApiResponse
{
    /// <summary>
    /// The <c>status</c> property. Always set to <c>Status.Error</c>.
    /// </summary>
    [JsonPropertyName("status")]
    public override HttpStatus ResponseStatus { get; } = HttpStatus.Error;
    /// <summary>
    /// The <c>error</c> property. An object representing the error returned by the API Call.
    /// </summary>
    [JsonPropertyName("error")]
    public HttpError Error { get; init; } = new();
}