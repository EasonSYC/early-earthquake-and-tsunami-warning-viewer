using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Telegram.Dtos.TsunamiInformation;
public record SimpleKind
{
    [JsonPropertyName("code")]
    public required string Code { get; init; }
    [JsonPropertyName("name")]
    public required string Name { get; init; }
}
