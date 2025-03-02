using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Dtos.DmdataComponent.Enum;

namespace EasonEetwViewer.Dmdata.Dtos.DmdataComponent;
/// <summary>
/// Describes the depth of an earthquake.
/// </summary>
public record Depth
{
    /// <summary>
    /// The property <c>type</c>. A constant <c>深さ</c> for depth.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; } = "深さ";
    /// <summary>
    /// The property <c>unit</c>. A constant <c>km</c> for depth.
    /// </summary>
    [JsonPropertyName("unit")]
    public string Unit { get; } = "km";
    /// <summary>
    /// The property <c>value</c>. The depth of the earthquake.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when unclear.
    /// </remarks>
    [JsonPropertyName("value")]
    public required int? Value { get; init; }
    /// <summary>
    /// The property <c>condition</c>. Describes abnormal behaviours of the depth.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when <see cref="Value"/> is not <see langword="null"/> and not 0 or 700.
    /// </remarks>
    [JsonPropertyName("condition")]
    public DepthCondition? Condition { get; init; }
}
