using System.Text.Json.Serialization;
using EasonEetwViewer.Telegram.Dtos.TsunamiInformation;

namespace EasonEetwViewer.Telegram.Dtos.TsunamiInformation;
public record Kind
{
    [JsonPropertyName("code")]
    public required string Code { get; init; }
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    [JsonPropertyName("lastKind")]
    public required SimpleKind LastKind { get; init; }
}
