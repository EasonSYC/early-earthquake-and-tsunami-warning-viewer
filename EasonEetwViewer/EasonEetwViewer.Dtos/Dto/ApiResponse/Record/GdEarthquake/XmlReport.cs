using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.Dto.ApiResponse.Record.GdEarthquake;
public class XmlReport
{
    [JsonPropertyName("head")]
    public required XmlHead Head { get; init; }
    [JsonPropertyName("control")]
    public required XmlControl Control { get; init; }
}
