using System.Text.Json.Serialization;
using EasonEetwViewer.Dtos.Dto.JsonTelegram.EewInformation.Enum.Range;

namespace EasonEetwViewer.Dtos.Dto.JsonTelegram.EewInformation;
public record Region
{
    [JsonPropertyName("code")]
    public required string Code { get; init; }
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    [JsonPropertyName("isPlum")]
    public required bool IsPlum { get; init; }
    [JsonPropertyName("isWarning")]
    public required bool IsWarning { get; init; }

    [JsonPropertyName("forecastMaxInt")]
    public required FromTo<IntensityLower, IntensityUpper> ForecastMaxInt { get; init; }
    [JsonPropertyName("forecastMaxLgInt")]
    public FromTo<LgIntensityLower, LgIntensityUpper>? ForecastMaxLgInt { get; init; }
    [JsonPropertyName("kind")]
    public SimpleKind Kind { get; init; }
    [JsonPropertyName("condition")]
    public string? Condition { get; init; }
    [JsonPropertyName("arrivalTime")]
    public DateTimeOffset? ArrivalTime { get; init; }
}
