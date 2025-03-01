using System.Text.Json.Serialization;
using EasonEetwViewer.Api.Dtos.ApiResponse.Enum;
using EasonEetwViewer.Api.Dtos.ApiResponse.ResponseBase;
using EasonEetwViewer.Dtos.Enum;

namespace EasonEetwViewer.Api.Dtos.ApiResponse.ResponseBase;

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