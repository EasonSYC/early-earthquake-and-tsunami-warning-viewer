using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.TelegramBase;
public record AdditionalComment
{
    [JsonPropertyName("text")]
    public required string Text { get; init; }
    [JsonPropertyName("codes")]
    public required List<string> Codes { get; init; }
}
