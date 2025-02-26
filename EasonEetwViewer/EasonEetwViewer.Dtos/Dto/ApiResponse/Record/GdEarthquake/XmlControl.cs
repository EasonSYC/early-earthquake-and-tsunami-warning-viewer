using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.TelegramBase;

namespace EasonEetwViewer.Dmdata.Dto.ApiResponse.Record.GdEarthquake;
public record XmlControl
{
    [JsonPropertyName("title")]
    public required string Title { get; init; }
    [JsonPropertyName("status")]
    public required Status Status { get; init; }
    [JsonPropertyName("dateTime")]
    public required DateTimeOffset Time { get; init; }
    [JsonPropertyName("editorialOffice")]
    public required string EditorialOffice { get; init; }
    [JsonPropertyName("publishingOffice")]
    public required string PublishingOFfice { get; init; }
}
