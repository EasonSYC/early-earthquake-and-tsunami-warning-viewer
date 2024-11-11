using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.Http.Response.Record;

/// <summary>
/// Represents a coordinate, either longitude or longitude.
/// </summary>
public record Coordinate
{
    /// <summary>
    /// The property <c>text</c>. The displayed text of the coordinate.
    /// </summary>
    [JsonPropertyName("text")]
    public required string DisplayText { get; init; }
    /// <summary>
    /// The property <c>value</c>. The double value of the coordinate.
    /// </summary>
    [JsonPropertyName("value")]
    public required double DoubleValue { get; init; }
}
