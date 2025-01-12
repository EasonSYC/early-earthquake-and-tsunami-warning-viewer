using System.Text.Json.Serialization;

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
