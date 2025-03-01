using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.JsonTelegram;

public record SchemaVersionInformation
{
    [JsonPropertyName("type")]
    public required string Type { get; init; }
    [JsonPropertyName("version")]
    public required string Version { get; init; }
}
