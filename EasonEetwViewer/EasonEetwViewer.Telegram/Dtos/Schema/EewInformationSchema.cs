using System.Text.Json.Serialization;
using EasonEetwViewer.Telegram.Dtos.EewInformation;
using EasonEetwViewer.Telegram.Dtos.TelegramBase;

namespace EasonEetwViewer.Telegram.Dtos.Schema;
public record EewInformationSchema : Head
{
    [JsonPropertyName("body")]
    public required Body Body { get; init; }
}
