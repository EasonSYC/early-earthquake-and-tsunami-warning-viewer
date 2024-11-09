using System.Text.Json.Serialization;
using EasonEetwViewer.Dto.Http.Response.Enum;

namespace EasonEetwViewer.Dto.Http.Response.Model;

/// <summary>
/// Outlines the model of a HTTP response in JSON format from an API Call.
/// Abstract and cannot be instantiated.
/// </summary>
public abstract record ApiResponse
{
    /// <summary>
    /// The <c>responseId</c> property. An unique ID for the API Call.
    /// </summary>
    [JsonPropertyName("responseId")]
    public required string ResponseId { get; init; }
    /// <summary>
    /// The <c>responseTime</c> property. The time when the call was received.
    /// </summary>
    [JsonPropertyName("responseTime")]
    public required DateTime ResponseTime { get; init; }
    /// <summary>
    /// The <c>status</c> property. An enum representing the status of the call.
    /// Abstract and has to be overridden.
    /// </summary>
    [JsonPropertyName("status")]
    public abstract HttpStatus ResponseStatus { get; }
}