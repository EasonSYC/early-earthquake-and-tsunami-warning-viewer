using System.Text.Json.Serialization;
using EasonEetwViewer.Telegram.Dtos.TsunamiInformation.Enum;

namespace EasonEetwViewer.Telegram.Dtos.TsunamiInformation;
public record MaxHeight
{
    [JsonPropertyName("height")]
    public required TsunamiHeight Height { get; init; }
    [JsonPropertyName("condition")]
    public MaxHeightCondition? Condition { get; init; }
    [JsonPropertyName("revise")]
    public Revise? Revise { get; init; }
}
