using EasonEetwViewer.Dto.Http.Response.Enums;
using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.Http.Response.Models;

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
    public string ResponseId { get; init; } = string.Empty;
    /// <summary>
    /// The <c>responseTime</c> property. The time when the call was received.
    /// </summary>
    [JsonPropertyName("responseTime")]
    public DateTime ResponseTime { get; init; }
    /// <summary>
    /// The <c>status</c> property. An enum representing the status of the call.
    /// Abstract and has to be overriden.
    /// </summary>
    [JsonPropertyName("status")]
    public abstract HttpStatus ResponseStatus { get; init; }
}