using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.Http.Response.Record;

/// <summary>
/// Represents auxiliary information provided on the position of the hypocentre.
/// </summary>
public record Auxiliary
{
    /// <summary>
    /// The property <c>text</c>. The position where the hypocentre is captured.
    /// </summary>
    [JsonPropertyName("text")]
    public required string Text { get; init; }
    /// <summary>
    /// The property <c>code</c>. The area code for which the hypocentre is captured.
    /// </summary>
    [JsonPropertyName("code")]
    public required int Code { get; init; }
    /// <summary>
    /// The property <c>name</c>. The area name for which the hypocentre is captured.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    /// <summary>
    /// The property <c>direction</c>. The direction in which the hypocentre is positioned relative to the area.
    /// </summary>
    [JsonPropertyName("direction")]
    public required string Direction { get; init; }
    /// <summary>
    /// The property <c>distance</c>. The distance of the hypocentre to the area of capture.
    /// </summary>
    [JsonPropertyName("distance")]
    public required Distance Distance { get; init; }
}
