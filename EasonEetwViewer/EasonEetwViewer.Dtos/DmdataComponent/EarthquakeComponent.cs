using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.DmdataComponent;
public record EarthquakeComponent
{
    [JsonPropertyName("originTime")]
    public required DateTimeOffset OriginTime { get; init; }
    [JsonPropertyName("arrivalTime")]
    public required DateTimeOffset ArrivalTime { get; init; }
    [JsonPropertyName("hypocenter")]
    public required Hypocentre Hypocentre { get; init; }
    [JsonPropertyName("magnitude")]
    public required Magnitude Magnitude { get; init; }
}
