using System.Text.Json.Serialization;
using EasonEetwViewer.Telegram.Dtos.TelegramBase;
using EasonEetwViewer.Telegram.Dtos.TsunamiInformation;

namespace EasonEetwViewer.Telegram.Dtos.Schema;
public record TsunamiInformationSchema : Head
{
    [JsonPropertyName("body")]
    public required Body Body { get; init; }
}
