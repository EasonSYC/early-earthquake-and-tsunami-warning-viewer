using System.Text.Json.Serialization;
using EasonEetwViewer.HttpRequest.Dto.Enum;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram.EarthquakeInformation;
public record IntensityDetails
{
    [JsonPropertyName("maxInt")]
    public required EarthquakeIntensity MaxInt { get; init; }
    [JsonPropertyName("prefectures")]
    public required List<RegionIntensity> Prefectures { get; init; }
    [JsonPropertyName("regions")]
    public required List<RegionIntensity> Regions { get; init; }
    [JsonPropertyName("cities")]
    public required List<CityIntensity> Cities { get; init; }
    [JsonPropertyName("stations")]
    public required List<StationIntensity> Stations { get; init; }
}
