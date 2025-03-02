using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Dtos.DmdataComponent.Enum;

namespace EasonEetwViewer.Dmdata.Dtos.DmdataComponent;
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
    public required CoordinateComponent Coordinates { get; init; }
    /// <summary>
    /// The property <c>depth</c>. The depth of the hypocentre.
    /// </summary>
    [JsonPropertyName("depth")]
    public required Depth Depth { get; init; }
    /// <summary>
    /// The property <c>detailed</c>. The extra details of the hypocentre.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when the earthquake happens within Japan.
    /// </remarks>
    [JsonPropertyName("detailed")]
    public HypocentreDetail? Detail { get; init; }
    /// <summary>
    /// The property <c>auxiliary</c>. The auxiliary information of the hypocentre.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> depending on situation.
    /// </remarks>
    [JsonPropertyName("auxiliary")]
    public Auxiliary? Auxiliary { get; init; }
    /// <summary>
    /// The property <c>source</c>. The source of the hypocentre information.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when the earthquake happens within Japan.
    /// </remarks>
    [JsonPropertyName("source")]
    public Source? Source { get; init; }
}
