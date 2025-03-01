using System.Text.Json.Serialization;
using EasonEetwViewer.Telegram.Dtos.EewInformation;

namespace EasonEetwViewer.Telegram.Dtos.EewInformation;
public record Body
{
    [JsonPropertyName("isLastInfo")]
    public required bool IsLastInfo { get; init; }
    [JsonPropertyName("isCanceled")]
    public required bool IsCancelled { get; init; }
    [JsonPropertyName("isWarning")]
    public bool? IsWarning { get; init; }
    [JsonPropertyName("zones")]
    public IEnumerable<Area>? Zones { get; init; }
    [JsonPropertyName("prefectures")]
    public IEnumerable<Area>? Prefectures { get; init; }
    [JsonPropertyName("regions")]
    public IEnumerable<Area>? Regions { get; init; }
    [JsonPropertyName("earthquake")]
    public Earthquake? Earthquake { get; init; }
    [JsonPropertyName("intensity")]
    public IntensityInfo? Intensity { get; init; }
    [JsonPropertyName("text")]
    public string? Text { get; init; }
    [JsonPropertyName("comments")]
    public Comments? Comments { get; init; }
}
