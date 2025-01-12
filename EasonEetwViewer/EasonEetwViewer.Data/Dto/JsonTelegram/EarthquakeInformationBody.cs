using System.Text.Json.Serialization;
using EasonEetwViewer.HttpRequest.DmdataComponent;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram;
public record EarthquakeInformationBody
{
    [JsonPropertyName("earthquake")]
    public EarthquakeComponent? Earthquake { get; init; }
    [JsonPropertyName("intensity")]
    public EarthquakeInformationIntensity? Intensity { get; init; }
    [JsonPropertyName("text")]
    public string? Text { get; init; }
    [JsonPropertyName("comments")]
    public EarthquakeInformationComments? Comments { get; init; }
}
