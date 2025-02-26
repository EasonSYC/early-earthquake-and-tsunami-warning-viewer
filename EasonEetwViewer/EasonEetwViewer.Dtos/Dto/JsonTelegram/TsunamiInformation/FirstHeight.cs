using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.TsunamiInformation.Enum;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.TsunamiInformation;
public record FirstHeight
{
    [JsonPropertyName("arrivalTime")]
    public DateTimeOffset? ArrivalTime { get; init; }
    [JsonPropertyName("condition")]
    public FirstHeightCondition? Condition { get; init; }
    [JsonPropertyName("revise")]
    public Revise? Revise { get; init; }
}
