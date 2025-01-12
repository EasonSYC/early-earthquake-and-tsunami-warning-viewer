using System.Text.Json.Serialization;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram;
public record TelegramAdditionalComment
{
    [JsonPropertyName("text")]
    public required string Text { get; init; }
    [JsonPropertyName("codes")]
    public required List<string> Codes { get; init; }
}
