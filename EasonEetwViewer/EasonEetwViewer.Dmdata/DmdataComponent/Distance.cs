using System.Text.Json.Serialization;

namespace EasonEetwViewer.HttpRequest.DmdataComponent;

/// <summary>
/// Represents a distance.
/// </summary>
public record Distance
{
    /// <summary>
    /// The property <c>unit</c>. A constant <c>km</c>.
    /// </summary>
    [JsonPropertyName("unit")]
    public string Unit { get; } = "km";
    /// <summary>
    /// The property <c>value</c>. The value of the distance.
    /// </summary>
    [JsonPropertyName("value")]
    public required int Value { get; set; }
}
