using System.Text.Json.Serialization;
using EasonEetwViewer.Dto.Http.Response.Enum;

namespace EasonEetwViewer.Dto.Http.Response.Model;

/// <summary>
/// Outlines the model of an Error HTTP response.
/// Abstract and cannot be instantiated.
/// </summary>
public abstract record SuccessResponse : ApiResponse
{
    /// <summary>
    /// The <c>status</c> property. Always set to <c>Status.Success</c>.
    /// </summary>
    [JsonPropertyName("status")]
    public override HttpStatus ResponseStatus { get; } = HttpStatus.Success;
}