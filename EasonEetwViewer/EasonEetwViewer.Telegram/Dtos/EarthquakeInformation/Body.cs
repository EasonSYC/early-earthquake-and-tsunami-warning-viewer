using System.Text.Json.Serialization;
using EasonEetwViewer.Dtos.DmdataComponent;
using EasonEetwViewer.Telegram.Dtos.EarthquakeInformation;

namespace EasonEetwViewer.Telegram.Dtos.EarthquakeInformation;
public record Body
{
    [JsonPropertyName("earthquake")]
    public EarthquakeComponent? Earthquake { get; init; }
    [JsonPropertyName("intensity")]
    public IntensityDetails? Intensity { get; init; }
    [JsonPropertyName("text")]
    public string? Text { get; init; }
    [JsonPropertyName("comments")]
    public Comments? Comments { get; init; }
}
