using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
