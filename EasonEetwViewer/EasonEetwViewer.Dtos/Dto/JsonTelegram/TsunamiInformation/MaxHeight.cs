using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.TsunamiInformation.Enum;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.TsunamiInformation;
public record MaxHeight
{
    [JsonPropertyName("height")]
    public required TsunamiHeight Height { get; init; }
    [JsonPropertyName("condition")]
    public MaxHeightCondition? Condition { get; init; }
    [JsonPropertyName("revise")]
    public Revise? Revise { get; init; }
}
