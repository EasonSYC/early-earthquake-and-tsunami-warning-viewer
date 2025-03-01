using System.Text.Json.Serialization;
using EasonEetwViewer.Dtos.Dto.JsonTelegram.TelegramBase;
using EasonEetwViewer.Telegram.Dtos.EewInformation;

namespace EasonEetwViewer.Telegram.Dtos.Schema;
public record EewInformationSchema : Head
{
    [JsonPropertyName("body")]
    public required Body Body { get; init; }
}
