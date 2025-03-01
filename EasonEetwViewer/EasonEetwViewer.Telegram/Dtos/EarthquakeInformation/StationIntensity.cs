using System.Text.Json.Serialization;
using EasonEetwViewer.Dtos.Enum;
using EasonEetwViewer.Telegram.Dtos.EarthquakeInformation.Enum;

namespace EasonEetwViewer.Telegram.Dtos.EarthquakeInformation;
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
    public IEnumerable<PrePeriod>? PrePreiods { get; init; }
    [JsonPropertyName("revise")]
    public ReviseStatus? Revise { get; init; }
    [JsonPropertyName("condition")]
    public string? Condition { get; init; }
}
