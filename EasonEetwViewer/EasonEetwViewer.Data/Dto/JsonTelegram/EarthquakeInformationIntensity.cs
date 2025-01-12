using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EasonEetwViewer.HttpRequest.Dto.Enum;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram;
public record EarthquakeInformationIntensity
{
    [JsonPropertyName("maxInt")]
    public required Enum.EarthquakeIntensity MaxInt { get; init; }
    [JsonPropertyName("prefectures")]
    public required List<EarthquakeInformationObservationData> Prefectures { get; init; }
    [JsonPropertyName("regions")]
    public required List<EarthquakeInformationObservationData> Regions { get; init; }
    [JsonPropertyName("cities")]
    public required List<EarthquakeInformationObservationDataWithCondition> Cities { get; init; }
    [JsonPropertyName("stations")]
    public required List<EarthquakeInformationObservationDataWithCondition> Stations { get; init; }
}
