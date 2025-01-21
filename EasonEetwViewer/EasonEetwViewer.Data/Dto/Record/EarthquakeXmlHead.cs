using System.Text.Json.Serialization;
using EasonEetwViewer.HttpRequest.Dto.JsonTelegram.TelegramBase;

namespace EasonEetwViewer.HttpRequest.Dto.Record;
public record EarthquakeXmlHead
{
    [JsonPropertyName("title")]
    public required string Title { get; init; }
    [JsonPropertyName("eventId")]
    public required string? EventId { get; init; }
    [JsonPropertyName("serial")]
    public required string? SerialNo { get; init; }
    [JsonPropertyName("headline")]
    public required string? Headline { get; init; }
    [JsonPropertyName("infoKind")]
    public required string InfoKind { get; init; }
    [JsonPropertyName("infoKindVersion")]
    public required string InfoKindVersion { get; init; }
    [JsonPropertyName("infoType")]
    public required JsonTelegram.TelegramBase.Type InfoType { get; init; }
    [JsonPropertyName("reportDateTime")]
    public required DateTimeOffset ReportDateTime { get; init; }
    [JsonPropertyName("targetDateTime")]
    public required DateTimeOffset? TargetDateTime { get; init; }
    [JsonPropertyName("targetDateTimeDubious")]
    public string? TargetDateTimeDubious { get; init; }
    [JsonPropertyName("targetDuration")]
    public string? TargetDuration { get; init; }
    [JsonPropertyName("validDateTime")]
    public DateTimeOffset? ValidDateTime { get; init; }
}
