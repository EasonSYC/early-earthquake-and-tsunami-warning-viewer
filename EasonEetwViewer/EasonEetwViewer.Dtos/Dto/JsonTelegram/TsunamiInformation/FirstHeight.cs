using System.Text.Json.Serialization;
using EasonEetwViewer.Dtos.Dto.JsonTelegram.TsunamiInformation.Enum;

namespace EasonEetwViewer.Dtos.Dto.JsonTelegram.TsunamiInformation;
public record FirstHeight
{
    [JsonPropertyName("arrivalTime")]
    public DateTimeOffset? ArrivalTime { get; init; }
    [JsonPropertyName("condition")]
    public FirstHeightCondition? Condition { get; init; }
    [JsonPropertyName("revise")]
    public Revise? Revise { get; init; }
}
