using System.Text.Json.Serialization;
using EasonEetwViewer.Telegram.Dtos.EewInformation;

namespace EasonEetwViewer.Telegram.Dtos.EewInformation;
public record Area
{
    [JsonPropertyName("code")]
    public required string Code { get; init; }
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    [JsonPropertyName("kind")]
    public required AreaKind Kind { get; init; }
}
