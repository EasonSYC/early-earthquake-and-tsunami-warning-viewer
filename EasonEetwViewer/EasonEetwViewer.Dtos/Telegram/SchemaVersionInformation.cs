using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.JsonTelegram;
/// <summary>
/// Represents the version of a JSON Telegram Schema.
/// </summary>
public record SchemaVersionInformation
{
    /// <summary>
    /// The property <c>type</c>, the type of the schema.
    /// </summary>
    [JsonPropertyName("type")]
    public required string Type { get; init; }
    /// <summary>
    /// The property <c>version</c>, the version of the schema.
    /// </summary>
    [JsonPropertyName("version")]
    public required string Version { get; init; }
}
