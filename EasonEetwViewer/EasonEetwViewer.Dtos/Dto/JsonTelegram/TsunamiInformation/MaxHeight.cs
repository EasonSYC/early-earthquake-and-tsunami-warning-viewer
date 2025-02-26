using System.Text.Json.Serialization;
using EasonEetwViewer.Dtos.Dto.JsonTelegram.TsunamiInformation.Enum;

namespace EasonEetwViewer.Dtos.Dto.JsonTelegram.TsunamiInformation;
public record MaxHeight
{
    [JsonPropertyName("height")]
    public required TsunamiHeight Height { get; init; }
    [JsonPropertyName("condition")]
    public MaxHeightCondition? Condition { get; init; }
    [JsonPropertyName("revise")]
    public Revise? Revise { get; init; }
}
