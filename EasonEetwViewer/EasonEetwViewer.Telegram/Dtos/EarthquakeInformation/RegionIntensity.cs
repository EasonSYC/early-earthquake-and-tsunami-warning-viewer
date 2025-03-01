using System.Text.Json.Serialization;
using EasonEetwViewer.Dtos.ApiResponse.Enum;
using EasonEetwViewer.Telegram.Dtos.EarthquakeInformation.Enum;

namespace EasonEetwViewer.Telegram.Dtos.EarthquakeInformation;

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
