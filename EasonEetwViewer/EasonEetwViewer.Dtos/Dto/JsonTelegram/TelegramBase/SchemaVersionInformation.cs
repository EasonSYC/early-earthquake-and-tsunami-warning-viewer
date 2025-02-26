using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.Dto.JsonTelegram.TelegramBase;

public record SchemaVersionInformation
{
    [JsonPropertyName("type")]
    public required string Type { get; init; }
    [JsonPropertyName("version")]
    public required string Version { get; init; }
}
