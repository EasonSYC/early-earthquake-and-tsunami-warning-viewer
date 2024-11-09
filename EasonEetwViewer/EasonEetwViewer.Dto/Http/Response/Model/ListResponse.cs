using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.Http.Response.Model;

/// <summary>
/// Outlines the model of a list response from an API call.
/// Abstract and cannot be instantiated.
/// </summary>
/// <typeparam name="T">The type of item in the list.</typeparam>
public abstract record ListResponse<T> : SuccessResponse
{
    /// <summary>
    /// The <c>items</c> property. The list of items returned by the API call.
    /// </summary>
    [JsonPropertyName("items")]
    public PrintList<T> ItemList { get; init; } = [];
}
