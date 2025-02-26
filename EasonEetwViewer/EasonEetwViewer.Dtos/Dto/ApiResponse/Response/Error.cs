using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Enum;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Record;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.ResponseBase;

namespace EasonEetwViewer.Dmdata.Dto.ApiResponse.Response;

/// <summary>
/// Represents an Error HTTP response.
/// </summary>
public record Error : ApiBase
{
    /// <summary>
    /// The <c>status</c> property. Always set to <c>Status.Error</c>.
    /// </summary>
    [JsonPropertyName("status")]
    public override ResponseStatus ResponseStatus { get; } = ResponseStatus.Error;
    /// <summary>
    /// The <c>error</c> property. An object representing the error returned by the API Call.
    /// </summary>
    [JsonPropertyName("error")]
    public required ErrorDetails ErrorDetails { get; init; }
}