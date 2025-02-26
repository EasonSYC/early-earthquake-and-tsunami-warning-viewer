using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.Dto.JsonTelegram.TelegramBase;
public record AdditionalComment
{
    [JsonPropertyName("text")]
    public required string Text { get; init; }
    [JsonPropertyName("codes")]
    public required IEnumerable<string> Codes { get; init; }
}
