using System.Text.Json.Serialization;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram;
public record EarthquakeInformationStationData
{
    [JsonPropertyName("code")]
    public required string Code { get; init; }
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    [JsonPropertyName("int")]
    public EarthquakeIntensityWithUnreceived MaxInt { get; init; }
    [JsonPropertyName("revise")]
    public EarthquakeInformationReviseStatus? Revise { get; init; }
    [JsonPropertyName("condition")]
    public string? Condition { get; init; }
}
