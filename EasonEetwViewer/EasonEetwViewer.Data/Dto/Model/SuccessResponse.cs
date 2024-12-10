using System.Text.Json.Serialization;
using EasonEetwViewer.HttpRequest.Dto.Enum;

namespace EasonEetwViewer.HttpRequest.Dto.Model;

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