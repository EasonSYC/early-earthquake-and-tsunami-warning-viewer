using System.Text.Json.Serialization;
using EasonEetwViewer.Dtos.Dto.JsonTelegram.TelegramBase;

namespace EasonEetwViewer.Dtos.Dto.JsonTelegram.EarthquakeInformation;
public record Comments
{
    [JsonPropertyName("free")]
    public string? FreeText { get; init; }
    [JsonPropertyName("forecast")]
    public AdditionalComment? Forecast { get; init; }
    [JsonPropertyName("var")]
    public AdditionalComment? Var { get; init; }
}
