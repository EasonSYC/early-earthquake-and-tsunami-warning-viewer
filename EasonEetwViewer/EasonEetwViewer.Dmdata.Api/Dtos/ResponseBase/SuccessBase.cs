using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Api.Dtos.Enum;

namespace EasonEetwViewer.Dmdata.Api.Dtos.ResponseBase;

/// <summary>
/// Outlines the model of an Error HTTP response.
/// Abstract and cannot be instantiated.
/// </summary>
public abstract record SuccessBase : ApiBase
{
    /// <summary>
    /// The <c>status</c> property. Always set to <see cref="ResponseStatus.Success"/>.
    /// </summary>
    [JsonPropertyName("status")]
    public override ResponseStatus ResponseStatus { get; } = ResponseStatus.Success;
}