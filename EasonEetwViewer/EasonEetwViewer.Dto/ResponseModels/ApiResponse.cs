using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.ResponseModels;

/// <summary>
/// Outlines the model of a HTTP response in JSON format from an API Call.
/// Abstract and cannot be instantiated.
/// </summary>
public abstract record ApiResponse
{
    /// <summary>
    /// The <c>responseId</c> property.
    /// </summary>
    [JsonPropertyName("responseId")]
    public string Id { get; init; } = string.Empty;
    /// <summary>
    /// The <c>responseTime</c> property.
    /// </summary>
    [JsonPropertyName("responseTime")]
    public DateTime Time { get; init; }
    /// <summary>
    /// The <c>status</c> property.
    /// </summary>
    [JsonPropertyName("status")]
    public ResponseEnums.Status Status { get; init; }
}