using System.Text.Json.Serialization;
using EasonEetwViewer.Dtos.Dto.ApiResponse.Enum;
using EasonEetwViewer.Dtos.Dto.JsonTelegram.EarthquakeInformation.Enum;

namespace EasonEetwViewer.Dtos.Dto.JsonTelegram.EarthquakeInformation;
public record CityIntensity
{
    [JsonPropertyName("code")]
    public required string Code { get; init; }
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    [JsonPropertyName("maxInt")]
    public Intensity? MaxInt { get; init; }
    [JsonPropertyName("revise")]
    public ReviseStatus? Revise { get; init; }
    [JsonPropertyName("condition")]
    public string? Condition { get; init; }
}
