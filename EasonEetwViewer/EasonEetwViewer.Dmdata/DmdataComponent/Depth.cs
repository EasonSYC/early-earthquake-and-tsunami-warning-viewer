using System.Text.Json.Serialization;
using EasonEetwViewer.HttpRequest.DmdataComponent.Enum;

namespace EasonEetwViewer.HttpRequest.DmdataComponent;
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
    /// <c>null</c> when unclear.
    /// </summary>
    [JsonPropertyName("value")]
    public required int? Value { get; init; }
    /// <summary>
    /// The property <c>condition</c>. Describes abnormal behaviours of the depth.
    /// <c>null</c> when the value is not <c>null</c> and not 0 or 700.
    /// </summary>
    [JsonPropertyName("condition")]
    public DepthCondition? Condition { get; init; }
}
