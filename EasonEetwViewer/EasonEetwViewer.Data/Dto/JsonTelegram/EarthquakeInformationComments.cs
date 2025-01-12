using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
