using System.Text.Json.Serialization;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram;
public record EarthquakeInformationComments
{
    [JsonPropertyName("free")]
    public string? FreeText { get; init; }
    [JsonPropertyName("forecast")]
    public TelegramAdditionalComment? Forecast { get; init; }
    [JsonPropertyName("var")]
    public TelegramAdditionalComment? Var { get; init; }
}
