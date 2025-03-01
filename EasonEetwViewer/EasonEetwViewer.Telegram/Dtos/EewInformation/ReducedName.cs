using System.Text.Json.Serialization;

namespace EasonEetwViewer.Telegram.Dtos.EewInformation;
public record ReducedName
{
    [JsonPropertyName("code")]
    public required string Code { get; init; }
    [JsonPropertyName("name")]
    public required string Name { get; init; }
}
