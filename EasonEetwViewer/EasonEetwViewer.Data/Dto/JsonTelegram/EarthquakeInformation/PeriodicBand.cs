using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram.EarthquakeInformation;
public record PeriodicBand
{
    [JsonPropertyName("unit")]
    public string Unit { get; } = "秒台";
    [JsonPropertyName("value")]
    public required int Value { get; init; }
}
