using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.JsonTelegram;
public record XmlControl
{
    [JsonPropertyName("title")]
    public required string Title { get; init; }
    [JsonPropertyName("status")]
    public required TelegramStatus Status { get; init; }
    [JsonPropertyName("dateTime")]
    public required DateTimeOffset Time { get; init; }
    [JsonPropertyName("editorialOffice")]
    public required string EditorialOffice { get; init; }
    [JsonPropertyName("publishingOffice")]
    public required string PublishingOFfice { get; init; }
}
