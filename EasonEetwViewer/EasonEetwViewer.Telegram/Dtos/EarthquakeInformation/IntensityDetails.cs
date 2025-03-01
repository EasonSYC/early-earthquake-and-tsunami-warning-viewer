using System.Text.Json.Serialization;
using EasonEetwViewer.Dtos.Dto.ApiResponse.Enum;

namespace EasonEetwViewer.Telegram.Dtos.EarthquakeInformation;
public record IntensityDetails
{
    [JsonPropertyName("maxInt")]
    public required Intensity MaxInt { get; init; }
    [JsonPropertyName("maxLgInt")]
    public LgIntensity? MaxLgInt { get; init; }
    [JsonPropertyName("lgCategory")]
    public LgCategory? LgCategory { get; init; }
    [JsonPropertyName("prefectures")]
    public required IEnumerable<RegionIntensity> Prefectures { get; init; }
    [JsonPropertyName("regions")]
    public required IEnumerable<RegionIntensity> Regions { get; init; }
    [JsonPropertyName("cities")]
    public required IEnumerable<CityIntensity> Cities { get; init; }
    [JsonPropertyName("stations")]
    public required IEnumerable<StationIntensity> Stations { get; init; }
}
