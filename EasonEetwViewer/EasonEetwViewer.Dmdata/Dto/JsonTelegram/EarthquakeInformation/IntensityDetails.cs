using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Enum;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.EarthquakeInformation;
public record IntensityDetails
{
    [JsonPropertyName("maxInt")]
    public required Intensity MaxInt { get; init; }
    [JsonPropertyName("maxLgInt")]
    public LgIntensity? MaxLgInt { get; init; }
    [JsonPropertyName("lgCategory")]
    public LgCategory? LgCategory { get; init; }
    [JsonPropertyName("prefectures")]
    public required List<RegionIntensity> Prefectures { get; init; }
    [JsonPropertyName("regions")]
    public required List<RegionIntensity> Regions { get; init; }
    [JsonPropertyName("cities")]
    public required List<CityIntensity> Cities { get; init; }
    [JsonPropertyName("stations")]
    public required List<StationIntensity> Stations { get; init; }
}
