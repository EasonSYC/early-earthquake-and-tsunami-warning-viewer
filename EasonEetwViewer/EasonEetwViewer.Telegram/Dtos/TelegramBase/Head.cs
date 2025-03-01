using System.Text.Json.Serialization;
using EasonEetwViewer.Dtos.Telegram;

namespace EasonEetwViewer.Telegram.Dtos.TelegramBase;

public record Head
{
    [JsonPropertyName("_originalId")]
    public required string OriginalId { get; init; }

    [JsonPropertyName("_schema")]
    public required SchemaVersionInformation Schema { get; init; }
    [JsonPropertyName("type")]
    public required string Type { get; init; }
    [JsonPropertyName("title")]
    public required string Title { get; init; }
    [JsonPropertyName("status")]
    public required TelegramStatus Status { get; init; }
    [JsonPropertyName("infoType")]
    public required TelegramType InfoType { get; init; }
    [JsonPropertyName("editorialOffice")]
    public required string EditorialOffice { get; init; }
    [JsonPropertyName("publishingOffice")]
    public required IEnumerable<string> PublishingOffice { get; init; }
    [JsonPropertyName("pressDateTime")]
    public required DateTimeOffset PressDateTime { get; init; }
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
    [JsonPropertyName("eventId")]
    public required string? EventId { get; init; }
    [JsonPropertyName("serialNo")]
    public required string? SerialNo { get; init; }
    [JsonPropertyName("infoKind")]
    public required string InfoKind { get; init; }
    [JsonPropertyName("infoKindVersion")]
    public required string InfoKindVersion { get; init; }
    [JsonPropertyName("headline")]
    public required string? Headline { get; init; }
}
