using System.Text.Json.Serialization;
using EasonEetwViewer.Dtos.Dto.JsonTelegram.EewInformation.Enum.Change;

namespace EasonEetwViewer.Dtos.Dto.JsonTelegram.EewInformation;
public record Appendix
{
    [JsonPropertyName("maxIntChange")]
    public required MaxInt MaxIntensityChange { get; init; }
    [JsonPropertyName("maxLgIntChange")]
    public MaxInt? MaxLgIntensityChange { get; init; }
    [JsonPropertyName("maxIntChangeReason")]
    public required Reason MaxIntensityChangeReason { get; init; }
}
