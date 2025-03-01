using System.Text.Json.Serialization;
using EasonEetwViewer.Telegram.Dtos.EewInformation.Enum.Change;

namespace EasonEetwViewer.Telegram.Dtos.EewInformation;
public record Appendix
{
    [JsonPropertyName("maxIntChange")]
    public required MaxInt MaxIntensityChange { get; init; }
    [JsonPropertyName("maxLgIntChange")]
    public MaxInt? MaxLgIntensityChange { get; init; }
    [JsonPropertyName("maxIntChangeReason")]
    public required Reason MaxIntensityChangeReason { get; init; }
}
