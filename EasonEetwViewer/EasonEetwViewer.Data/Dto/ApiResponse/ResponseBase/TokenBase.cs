using System.Text.Json.Serialization;

namespace EasonEetwViewer.HttpRequest.Dto.ApiResponse.ResponseBase;

/// <summary>
/// Outlines the model of a list response with next token options.
/// Abstract and cannot be instantiated.
/// </summary>
/// <typeparam name="T">The type of item in the list.</typeparam>
public abstract record TokenBase<T> : ListBase<T>
{
    /// <summary>
    /// The <c>nextToken</c> property. The token that should be specified in the next API call.
    /// <c>null</c> when the current call is the final call.
    /// </summary>
    [JsonPropertyName("nextToken")]
    public string? NextToken { get; init; }
}