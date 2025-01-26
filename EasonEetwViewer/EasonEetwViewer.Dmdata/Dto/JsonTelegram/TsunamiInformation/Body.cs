using System.Text.Json.Serialization;
using EasonEetwViewer.HttpRequest.DmdataComponent;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram.TsunamiInformation;
public record Body
{
    [JsonPropertyName("tsunami")]
    public Tsunami? Tsunami { get; init; }
    [JsonPropertyName("earthquakes")]
    public List<EarthquakeComponent>? Earthquakes { get; init; }
    [JsonPropertyName("text")]
    public string? Text { get; init; }
    [JsonPropertyName("comments")]
    public Comments? Comments { get; init; }
}
