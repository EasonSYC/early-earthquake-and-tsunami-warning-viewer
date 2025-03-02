using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dtos.DmdataComponent;
/// <summary>
/// Describes an earthquake.
/// </summary>
public record EarthquakeComponent
{
    /// <summary>
    /// The property <c>originTime</c>, the origin time of the earthquake.
    /// </summary>
    [JsonPropertyName("originTime")]
    public required DateTimeOffset OriginTime { get; init; }
    /// <summary>
    /// The property <c>arrivalTime</c>, the arrival time of the earthquake.
    /// </summary>
    [JsonPropertyName("arrivalTime")]
    public required DateTimeOffset ArrivalTime { get; init; }
    /// <summary>
    /// The property <c>hypocenter</c>, the hypocentre of the earthquake.
    /// </summary>
    [JsonPropertyName("hypocenter")]
    public required Hypocentre Hypocentre { get; init; }
    /// <summary>
    /// The property <c>magnitude</c>, the magnitude of the earthquake.
    /// </summary>
    [JsonPropertyName("magnitude")]
    public required Magnitude Magnitude { get; init; }
}
