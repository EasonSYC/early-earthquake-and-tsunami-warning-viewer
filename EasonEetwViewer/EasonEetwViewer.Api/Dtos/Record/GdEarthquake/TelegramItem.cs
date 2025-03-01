using System.Text.Json.Serialization;
using EasonEetwViewer.Dtos.Enum;
using EasonEetwViewer.Dtos.Enum.WebSocket;
using EasonEetwViewer.Dtos.JsonTelegram;

namespace EasonEetwViewer.Api.Dtos.Record.GdEarthquake;
public record TelegramItem
{
    [JsonPropertyName("serial")]
    public required int Serial { get; init; }
    [JsonPropertyName("id")]
    public required string Id { get; init; }
    [JsonPropertyName("originalId")]
    public required string OriginalId { get; init; }
    [JsonPropertyName("classification")]
    public required Classification Classification { get; init; }
    [JsonPropertyName("head")]
    public required TelegramHead TelegramHead { get; init; }
    [JsonPropertyName("receivedTime")]
    public required DateTimeOffset ReceivedTime { get; init; }
    [JsonPropertyName("xmlReport")]
    public required XmlReport XmlReport { get; init; }
    [JsonPropertyName("schema")]
    public required SchemaVersionInformation SchemaVersion { get; init; }
    [JsonPropertyName("format")]
    public required FormatType Format { get; init; }
    [JsonPropertyName("url")]
    public required Uri Url { get; init; }
}
