using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Enum;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.EarthquakeInformation.Enum;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.EarthquakeInformation;

public record RegionIntensity
{
    [JsonPropertyName("code")]
    public required string Code { get; init; }
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    [JsonPropertyName("maxInt")]
    public Intensity? MaxInt { get; init; }
    [JsonPropertyName("maxLgInt")]
    public LgIntensity? MaxLgInt { get; init; }
    [JsonPropertyName("revise")]
    public ReviseStatus? Revise { get; init; }
}
