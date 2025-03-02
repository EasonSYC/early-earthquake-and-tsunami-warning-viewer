using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dtos.Telegram;
/// <summary>
/// Describes the control of a XML telegram.
/// </summary>
public record XmlControl
{
    /// <summary>
    /// The property <c>title</c>, the title of the telegram.
    /// </summary>
    [JsonPropertyName("title")]
    public required string Title { get; init; }
    /// <summary>
    /// The property <c>status</c>, the status of the telegram.
    /// </summary>
    [JsonPropertyName("status")]
    public required TelegramStatus Status { get; init; }
    /// <summary>
    /// The property <c>dateTime</c>, the date and time of the telegram.
    /// </summary>
    [JsonPropertyName("dateTime")]
    public required DateTimeOffset Time { get; init; }
    /// <summary>
    /// The property <c>editorialOffice</c>, the editorial office of the telegram.
    /// </summary>
    [JsonPropertyName("editorialOffice")]
    public required string EditorialOffice { get; init; }
    /// <summary>
    /// The property <c>publishingOffice</c>, the publishing office of the telegram.
    /// </summary>
    [JsonPropertyName("publishingOffice")]
    public required string PublishingOFfice { get; init; }
}
