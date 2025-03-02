using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Dtos.DmdataComponent;

namespace EasonEetwViewer.Dmdata.Telegram.Dtos.TsunamiInformation;
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
