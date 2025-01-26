using System.Text.Json.Serialization;
using EasonEetwViewer.HttpRequest.Dto.ApiResponse.Enum;
using EasonEetwViewer.HttpRequest.Dto.JsonTelegram.EarthquakeInformation.Enum;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram.EarthquakeInformation;
public record StationIntensity
{
    [JsonPropertyName("code")]
    public required string Code { get; init; }
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    [JsonPropertyName("int")]
    public IntensityWithUnreceived? MaxInt { get; init; }
    [JsonPropertyName("lgInt")]
    public LgIntensity? LgInt { get; init; }
    [JsonPropertyName("sva")]
    public Sva? Sva { get; init; }
    [JsonPropertyName("prePeriods")]
    public List<PrePeriod>? PrePreiods { get; init; }
    [JsonPropertyName("revise")]
    public ReviseStatus? Revise { get; init; }
    [JsonPropertyName("condition")]
    public string? Condition { get; init; }
}
