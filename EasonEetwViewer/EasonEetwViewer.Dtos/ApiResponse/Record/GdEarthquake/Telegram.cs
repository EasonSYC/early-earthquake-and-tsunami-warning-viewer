using System.Text.Json.Serialization;
using EasonEetwViewer.Dtos.ApiResponse.Enum;
using EasonEetwViewer.Dtos.ApiResponse.Enum.WebSocket;
using EasonEetwViewer.Dtos.JsonTelegram;

namespace EasonEetwViewer.Dtos.ApiResponse.Record.GdEarthquake;
public record Telegram
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
