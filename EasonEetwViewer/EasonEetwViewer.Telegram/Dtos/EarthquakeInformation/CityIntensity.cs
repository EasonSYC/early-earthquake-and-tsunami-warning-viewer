using System.Text.Json.Serialization;
using EasonEetwViewer.Dtos.Dto.ApiResponse.Enum;
using EasonEetwViewer.Telegram.Dtos.EarthquakeInformation.Enum;

namespace EasonEetwViewer.Telegram.Dtos.EarthquakeInformation;
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
