using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Dtos.Enum;
using EasonEetwViewer.Dmdata.Dtos.Enum.WebSocket;
using EasonEetwViewer.Dmdata.Dtos.Telegram;

namespace EasonEetwViewer.Dmdata.Api.Dtos.Record.GdEarthquake;
/// <summary>
/// Represents an item in the list of telegrams for <c>gd.earthquake.event</c>.
/// </summary>
public record TelegramItem
{
    /// <summary>
    /// The property <c>serial</c>. The serial number of the telegram.
    /// </summary>
    [JsonPropertyName("serial")]
    public required int Serial { get; init; }
    /// <summary>
    /// The property <c>id</c>. The ID of the JSON version of the telegram.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }
    /// <summary>
    /// The property <c>originalId</c>. The original ID of the XML version of the telegram.
    /// </summary>
    [JsonPropertyName("originalId")]
    public required string OriginalId { get; init; }
    /// <summary>
    /// The property <c>classification</c>. The classification of the telegram.
    /// </summary>
    [JsonPropertyName("classification")]
    public required Classification Classification { get; init; }
    /// <summary>
    /// The property <c>head</c>. The head data of the telegram.
    /// </summary>
    [JsonPropertyName("head")]
    public required TelegramHead TelegramHead { get; init; }
    /// <summary>
    /// The property <c>receivedTime</c>. The time when the telegram was received.
    /// </summary>
    [JsonPropertyName("receivedTime")]
    public required DateTimeOffset ReceivedTime { get; init; }
    /// <summary>
    /// The property <c>xmlReport</c>. The XML head and control of the original XML telegram.
    /// </summary>
    [JsonPropertyName("xmlReport")]
    public required XmlReport XmlReport { get; init; }
    /// <summary>
    /// The property <c>schema</c>. The schema version of the JSON telegram.
    /// </summary>
    [JsonPropertyName("schema")]
    public required SchemaVersionInformation SchemaVersion { get; init; }
    /// <summary>
    /// The property <c>format</c>. The format of the telegram.
    /// </summary>
    [JsonPropertyName("format")]
    public required FormatType Format { get; init; }
    /// <summary>
    /// The property <c>url</c>. The URL of the JSON telegram.
    /// </summary>
    [JsonPropertyName("url")]
    public required Uri Url { get; init; }
}
