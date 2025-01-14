using System.Text.Json.Serialization;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram;

public record EarthquakeInformationRegionData
{
    [JsonPropertyName("code")]
    public required string Code { get; init; }
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    [JsonPropertyName("maxInt")]
    public Enum.EarthquakeIntensity? MaxInt { get; init; }
    [JsonPropertyName("revise")]
    public EarthquakeInformationReviseStatus? Revise { get; init; }
}
