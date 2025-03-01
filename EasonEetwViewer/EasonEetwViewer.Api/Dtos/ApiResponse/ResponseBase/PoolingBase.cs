using System.Text.Json.Serialization;
using EasonEetwViewer.Api.Dtos.ApiResponse.ResponseBase;

namespace EasonEetwViewer.Api.Dtos.ApiResponse.ResponseBase;

/// <summary>
/// Outlines the model of a list response with next pooling options.
/// Abstract and cannot be instantiated.
/// </summary>
/// <typeparam name="T">The type of item in the list.</typeparam>
public abstract record PoolingBase<T> : ListBase<T>
{
    /// <summary>
    /// The <c>nextToken</c> property. The token that should be specified in the next API call.
    /// <c>null</c> if it is a subsequent call using pooling, or if the current call is the final call.
    /// </summary>
    [JsonPropertyName("nextToken")]
    public string? NextToken { get; init; }
    /// <summary>
    /// The <c>nextPooling</c> property. The pooling token that should be specified in the next API call.
    /// <c>null</c> when the current call is the final call.
    /// </summary>
    [JsonPropertyName("nextPooling")]
    public string? NextPooling { get; init; }
    /// <summary>
    /// The <c>nextPoolingInterval</c> property. The time in milliseconds that the program should wait until the next API call.
    /// <c>null</c> when the current call is the final call.
    /// </summary>
    [JsonPropertyName("nextPoolingInterval")]
    public int? NextPoolingInterval { get; init; }
}