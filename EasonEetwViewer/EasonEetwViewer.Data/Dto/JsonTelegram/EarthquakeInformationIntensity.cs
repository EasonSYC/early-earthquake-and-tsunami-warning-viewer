using System.Text.Json.Serialization;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram;
public record EarthquakeInformationIntensity
{
    [JsonPropertyName("maxInt")]
    public required Enum.EarthquakeIntensity MaxInt { get; init; }
    [JsonPropertyName("prefectures")]
    public required List<EarthquakeInformationRegionData> Prefectures { get; init; }
    [JsonPropertyName("regions")]
    public required List<EarthquakeInformationRegionData> Regions { get; init; }
    [JsonPropertyName("cities")]
    public required List<EarthquakeInformationCityData> Cities { get; init; }
    [JsonPropertyName("stations")]
    public required List<EarthquakeInformationStationData> Stations { get; init; }
}
