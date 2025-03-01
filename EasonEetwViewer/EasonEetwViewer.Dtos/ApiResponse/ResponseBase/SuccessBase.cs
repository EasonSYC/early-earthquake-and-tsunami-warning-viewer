using System.Text.Json.Serialization;
using EasonEetwViewer.Dtos.ApiResponse.Enum;

namespace EasonEetwViewer.Dtos.ApiResponse.ResponseBase;

/// <summary>
/// Outlines the model of an Error HTTP response.
/// Abstract and cannot be instantiated.
/// </summary>
public abstract record SuccessBase : ApiBase
{
    /// <summary>
    /// The <c>status</c> property. Always set to <c>Status.Success</c>.
    /// </summary>
    [JsonPropertyName("status")]
    public override ResponseStatus ResponseStatus { get; } = ResponseStatus.Success;
}