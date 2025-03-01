using System.Text.Json.Serialization;
using EasonEetwViewer.Telegram.Dtos.TsunamiInformation.Enum;

namespace EasonEetwViewer.Telegram.Dtos.TsunamiInformation;
public record FirstHeight
{
    [JsonPropertyName("arrivalTime")]
    public DateTimeOffset? ArrivalTime { get; init; }
    [JsonPropertyName("condition")]
    public FirstHeightCondition? Condition { get; init; }
    [JsonPropertyName("revise")]
    public Revise? Revise { get; init; }
}
