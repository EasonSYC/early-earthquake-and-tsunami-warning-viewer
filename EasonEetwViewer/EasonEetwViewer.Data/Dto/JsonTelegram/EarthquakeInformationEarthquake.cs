using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EasonEetwViewer.HttpRequest.Dto.Record;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram;
public record EarthquakeInformationEarthquake
{
    [JsonPropertyName("originTime")]
    public required DateTime OriginTime { get; init; }
    [JsonPropertyName("arrivalTime")]
    public required DateTime ArrivalTime { get; init; }
    [JsonPropertyName("hypocenter")]
    public required EarthquakeHypocentre Hypocentre { get; init; }
    [JsonPropertyName("magnitude")]
    public required EarthquakeMagnitude Magnitude { get; init; }
}
