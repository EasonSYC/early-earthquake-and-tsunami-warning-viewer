using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.ResponseModels;

/// <summary>
/// Outlines the model of a list response with next pooling options.
/// Inherits from <c>ResponseModels.ListResponse</c>.
/// Abstract and cannot be instantiated.
/// </summary>
/// <typeparam name="T">The type of item in the list.</typeparam>
public abstract record PoolingResponse<T> : ListResponse<T>
{
    /// <summary>
    /// The <c>nextToken</c> property. The token that should be specified in the next API call.
    /// <c>null</c> if it is a subsequent call using pooling.
    /// </summary>
    [JsonPropertyName("nextToken")]
    public string? NextToken { get; init; }
    /// <summary>
    /// The <c>nextPooling</c> property. The pooling token that should be specified in the next API call.
    /// </summary>
    [JsonPropertyName("nextPooling")]
    public string NextPooling { get; init; } = string.Empty;
    /// <summary>
    /// The <c>nextPoolingInterval</c> property. The time in milliseconds that the program should wait until the next API call.
    /// </summary>
    [JsonPropertyName("nextPoolingInterval")]
    public int NextPoolingInterval { get; init; }
}