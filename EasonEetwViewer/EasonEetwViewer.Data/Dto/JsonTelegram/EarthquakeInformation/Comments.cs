using System.Text.Json.Serialization;
using EasonEetwViewer.HttpRequest.Dto.JsonTelegram.TelegramBase;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram.EarthquakeInformation;
public record Comments
{
    [JsonPropertyName("free")]
    public string? FreeText { get; init; }
    [JsonPropertyName("forecast")]
    public AdditionalComment? Forecast { get; init; }
    [JsonPropertyName("var")]
    public AdditionalComment? Var { get; init; }
}
