using System.Text.Json.Serialization;
using EasonEetwViewer.Api.Dtos.ApiResponse.ResponseBase;

namespace EasonEetwViewer.Api.Dtos.ApiResponse.ResponseBase;

/// <summary>
/// Outlines the model of a list response from an API call.
/// Abstract and cannot be instantiated.
/// </summary>
/// <typeparam name="T">The type of item in the list.</typeparam>
public abstract record ListBase<T> : SuccessBase
{
    /// <summary>
    /// The <c>items</c> property. The list of items returned by the API call.
    /// </summary>
    [JsonPropertyName("items")]
    public required IEnumerable<T> ItemList { get; init; }
}
