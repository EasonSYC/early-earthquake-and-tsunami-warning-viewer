using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EasonEetwViewer.HttpRequest.Dto.Enum;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram.EarthquakeInformation;
public record PrePeriod
{
    [JsonPropertyName("periodicBand")]
    public required PeriodicBand Band { get; init; }
    [JsonPropertyName("lgInt")]
    public required EarthquakeLgIntensity LgInt { get; init; }
    [JsonPropertyName("sva")]
    public required Sva Sva { get; init; }
}
