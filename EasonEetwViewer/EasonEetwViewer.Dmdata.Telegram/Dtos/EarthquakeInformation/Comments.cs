using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Telegram.Dtos.TelegramBase;

namespace EasonEetwViewer.Dmdata.Telegram.Dtos.EarthquakeInformation;
public record Comments
{
    [JsonPropertyName("free")]
    public string? FreeText { get; init; }
    [JsonPropertyName("forecast")]
    public AdditionalComment? Forecast { get; init; }
    [JsonPropertyName("var")]
    public AdditionalComment? Var { get; init; }
}
