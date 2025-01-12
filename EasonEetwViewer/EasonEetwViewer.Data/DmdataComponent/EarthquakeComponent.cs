using System.Text.Json.Serialization;

namespace EasonEetwViewer.HttpRequest.DmdataComponent;
public record EarthquakeComponent
{
    [JsonPropertyName("originTime")]
    public required DateTime OriginTime { get; init; }
    [JsonPropertyName("arrivalTime")]
    public required DateTime ArrivalTime { get; init; }
    [JsonPropertyName("hypocenter")]
    public required Hypocentre Hypocentre { get; init; }
    [JsonPropertyName("magnitude")]
    public required Magnitude Magnitude { get; init; }
}
