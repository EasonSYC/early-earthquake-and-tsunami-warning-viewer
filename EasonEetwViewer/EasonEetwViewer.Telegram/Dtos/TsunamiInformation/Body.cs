using System.Text.Json.Serialization;
using EasonEetwViewer.Dtos.DmdataComponent;
using EasonEetwViewer.Telegram.Dtos.TsunamiInformation;

namespace EasonEetwViewer.Telegram.Dtos.TsunamiInformation;
public record Body
{
    [JsonPropertyName("tsunami")]
    public Tsunami? Tsunami { get; init; }
    [JsonPropertyName("earthquakes")]
    public IEnumerable<EarthquakeComponent>? Earthquakes { get; init; }
    [JsonPropertyName("text")]
    public string? Text { get; init; }
    [JsonPropertyName("comments")]
    public Comments? Comments { get; init; }
}
