using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.ResponseModels;

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
    public override ResponseEnums.Status ResponseStatus { get; init; } = ResponseEnums.Status.Success;
}