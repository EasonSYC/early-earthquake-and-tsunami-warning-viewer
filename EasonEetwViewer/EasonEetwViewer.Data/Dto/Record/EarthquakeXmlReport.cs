using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EasonEetwViewer.HttpRequest.Dto.Record;
public class EarthquakeXmlReport
{
    [JsonPropertyName("head")]
    public required EarthquakeXmlHead Head { get; init; }
    [JsonPropertyName("control")]
    public required EarthquakeXmlControl Control { get; init; }
}
