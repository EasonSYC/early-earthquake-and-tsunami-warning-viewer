using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.Http.Response.Record;

/// <summary>
/// Represents the height of a position.
/// </summary>
public record Height
{
    /// <summary>
    /// The property <c>type</c>. A constant <c>高さ</c> for the height.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; } = "高さ";
    /// <summary>
    /// The property <c>unit</c>. A constant <c>m</c> for the height.
    /// </summary>
    [JsonPropertyName("unit")]
    public string Unit { get; } = "m";
    /// <summary>
    /// The property <c>value</c>. The value of the height.
    /// </summary>
    [JsonPropertyName("value")]
    public required float Value { get; init; }
}
