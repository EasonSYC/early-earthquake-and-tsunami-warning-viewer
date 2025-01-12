using System.Text.Json.Serialization;

namespace EasonEetwViewer.HttpRequest.Dto.Record;
public class EarthquakeXmlReport
{
    [JsonPropertyName("head")]
    public required EarthquakeXmlHead Head { get; init; }
    [JsonPropertyName("control")]
    public required EarthquakeXmlControl Control { get; init; }
}
