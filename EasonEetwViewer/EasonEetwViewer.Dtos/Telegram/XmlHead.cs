using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.Telegram;
/// <summary>
/// Represents the head information of a XML telegram.
/// </summary>
public record XmlHead
{
    /// <summary>
    /// The property <c>title</c>, the title of the telegram.
    /// </summary>
    [JsonPropertyName("title")]
    public required string Title { get; init; }
    /// <summary>
    /// The property <c>eventId</c>, the target event ID for the telegram.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when it has no target event.
    /// </remarks>
    [JsonPropertyName("eventId")]
    public required string? EventId { get; init; }
    /// <summary>
    /// The property <c>serial</c>, the serial number of the telegram for the event.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when it has no target event.
    /// </remarks>
    [JsonPropertyName("serial")]
    public required string? SerialNo { get; init; }
    /// <summary>
    /// The property <c>headline</c>, the headline of the telegram.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when no headline is provided.
    /// </remarks>
    [JsonPropertyName("headline")]
    public required string? Headline { get; init; }
    /// <summary>
    /// The property <c>infoKind</c>, the schema name for the XML telegram.
    /// </summary>
    [JsonPropertyName("infoKind")]
    public required string InfoKind { get; init; }
    /// <summary>
    /// The property <c>infoKindVersion</c>, the schema version for the XML telegram.
    /// </summary>
    [JsonPropertyName("infoKindVersion")]
    public required string InfoKindVersion { get; init; }
    /// <summary>
    /// The property <c>infoType</c>, the type of the telegram.
    /// </summary>
    [JsonPropertyName("infoType")]
    public required TelegramType InfoType { get; init; }
    /// <summary>
    /// The property <c>reportDateTime</c>, the release date and time of the telegram.
    /// </summary>
    [JsonPropertyName("reportDateTime")]
    public required DateTimeOffset ReportDateTime { get; init; }
    /// <summary>
    /// The property <c>targetDateTime</c>, the target date and time for the telegram.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when it has no target date or time.
    /// </remarks>
    [JsonPropertyName("targetDateTime")]
    public required DateTimeOffset? TargetDateTime { get; init; }
    /// <summary>
    /// The property <c>targetDateTimeDubious</c>, the target date and time for the telegram with dubiousness.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when it has no target date or time, or when it has no dubiousness.
    /// </remarks>
    [JsonPropertyName("targetDateTimeDubious")]
    public string? TargetDateTimeDubious { get; init; }
    /// <summary>
    /// The property <c>targetDuration</c>, the target duration for the telegram.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when it has no target duration.
    /// </remarks>
    [JsonPropertyName("targetDuration")]
    public string? TargetDuration { get; init; }
    /// <summary>
    /// The property <c>validDateTime</c>, the date and time the telegram is valid until.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when it has no target duration.
    /// </remarks>
    [JsonPropertyName("validDateTime")]
    public DateTimeOffset? ValidDateTime { get; init; }
}
