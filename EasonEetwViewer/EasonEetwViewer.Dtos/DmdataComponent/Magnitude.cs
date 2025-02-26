using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.DmdataComponent.Enum;

namespace EasonEetwViewer.Dmdata.DmdataComponent;
/// <summary>
/// Describes the magnitude of an earthquake.
/// </summary>
public record Magnitude
{
    /// <summary>
    /// The property <c>type</c>. A constant <c>マグニチュード</c> for magnitude.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; } = "マグニチュード";
    /// <summary>
    /// The property <c>unit</c>. The units of the magnitude.
    /// </summary>
    [JsonPropertyName("unit")]
    public required MagnitudeUnit Unit { get; init; }
    /// <summary>
    /// The property <c>value</c>. The magnitude of the earthquake.
    /// <c>null</c> when unclear or greater than M8.
    /// </summary>
    [JsonPropertyName("value")]
    public required float? Value { get; init; }
    /// <summary>
    /// The property <c>condition</c>. Abnormal behaviours of the magnitude.
    /// <c>null</c> when the value is not <c>null</c>.
    /// </summary>
    [JsonPropertyName("condition")]
    public MagnitudeCondition? Condition { get; init; }
}
