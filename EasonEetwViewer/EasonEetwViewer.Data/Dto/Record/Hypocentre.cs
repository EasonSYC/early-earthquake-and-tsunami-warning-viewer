using System.Text.Json.Serialization;
using EasonEetwViewer.HttpRequest.Dto.Enum;

namespace EasonEetwViewer.HttpRequest.Dto.Record;
/// <summary>
/// Represents the hypocentre of an earthquake.
/// </summary>
public record Hypocentre
{
    /// <summary>
    /// The property <c>name</c>. The name of the area of the hypocentre.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    /// <summary>
    /// The property <c>code</c>. The XML code of the area of the hypocentre.
    /// </summary>
    [JsonPropertyName("code")]
    public required int Code { get; init; }
    /// <summary>
    /// The property <c>coordinate</c>. The position of the hypocentre.
    /// </summary>
    [JsonPropertyName("coordinate")]
    public required CoordinatePosition Coordinates { get; init; }
    /// <summary>
    /// The property <c>depth</c>. The depth of the hypocentre.
    /// </summary>
    [JsonPropertyName("depth")]
    public required Depth Depth { get; init; }
    /// <summary>
    /// The property <c>detailed</c>. The extra details of the hypocentre.
    /// <c>null</c> when the earthquake happens within Japan.
    /// </summary>
    [JsonPropertyName("detailed")]
    public HypocentreDetail? Detail { get; init; }
    /// <summary>
    /// The property <c>auxiliary</c>. The auxiliary information of the hypocentre.
    /// <c>null</c> depending on situation.
    /// </summary>
    [JsonPropertyName("auxiliary")]
    public Auxiliary? Auxiliary { get; init; }
    /// <summary>
    /// The property <c>source</c>. The source of the hypocentre information.
    /// <c>null</c> when the earthquake happens within Japan.
    /// </summary>
    [JsonPropertyName("source")]
    public Source? Source { get; init; }
}
