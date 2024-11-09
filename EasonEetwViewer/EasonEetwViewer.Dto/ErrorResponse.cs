using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto;

/// <summary>
/// Represents an Error HTTP response.
/// Inherits from <c>ResponseModels.ApiResponse</c>.
/// </summary>
public record ErrorResponse : ResponseModels.ApiResponse
{
    /// <summary>
    /// The <c>status</c> property. Always set to <c>Status.Error</c>.
    /// </summary>
    [JsonPropertyName("status")]
    public override ResponseEnums.HttpStatus ResponseStatus { get; init; } = ResponseEnums.HttpStatus.Error;
    /// <summary>
    /// The <c>error</c> property. An object representing the error returned by the API Call.
    /// </summary>
    [JsonPropertyName("error")]
    public HttpError Error { get; init; } = new();
}