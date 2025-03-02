using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Telegram.Dtos.TelegramBase;
using EasonEetwViewer.Dmdata.Telegram.Dtos.TsunamiInformation;

namespace EasonEetwViewer.Dmdata.Telegram.Dtos.Schema;
public record TsunamiInformationSchema : Head
{
    [JsonPropertyName("body")]
    public required Body Body { get; init; }
}
