using EasonEetwViewer.Dto.Http.Response.Enums;
using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.Http.Response.Models;

/// <summary>
/// Outlines the model of an Error HTTP response.
/// Inherits from <c>ResponseModels.ApiResponse</c>.
/// Abstract and cannot be instantiated.
/// </summary>
public abstract record SuccessResponse : ApiResponse
{
    /// <summary>
    /// The <c>status</c> property. Always set to <c>Status.Success</c>.
    /// </summary>
    [JsonPropertyName("status")]
    public override HttpStatus ResponseStatus { get; init; } = HttpStatus.Success;
}