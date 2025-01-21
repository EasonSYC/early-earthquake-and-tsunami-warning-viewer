using System.Text.Json.Serialization;
using EasonEetwViewer.HttpRequest.Dto.Enum;
using EasonEetwViewer.HttpRequest.Dto.JsonTelegram.TelegramBase;

namespace EasonEetwViewer.HttpRequest.Dto.Record;
public record EarthquakeTelegram
{
    [JsonPropertyName("serial")]
    public required int Serial { get; init; }
    [JsonPropertyName("id")]
    public required string Id { get; init; }
    [JsonPropertyName("originalId")]
    public required string OriginalId { get; init; }
    [JsonPropertyName("classification")]
    public required ContractClassification Classification { get; init; }
    [JsonPropertyName("head")]
    public required EarthquakeTelegramHead TelegramHead { get; init; }
    [JsonPropertyName("receivedTime")]
    public required DateTimeOffset ReceivedTime { get; init; }
    [JsonPropertyName("xmlReport")]
    public required EarthquakeXmlReport XmlReport { get; init; }
    [JsonPropertyName("schema")]
    public required SchemaVersionInformation SchemaVersion { get; init; }
    [JsonPropertyName("format")]
    public required WebSocketFormatType Format { get; init; }
    [JsonPropertyName("url")]
    public required Uri Url { get; init; }
}
