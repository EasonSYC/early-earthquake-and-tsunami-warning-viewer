using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Telegram.Dtos.EewInformation;
using EasonEetwViewer.Dmdata.Telegram.Dtos.TelegramBase;

namespace EasonEetwViewer.Dmdata.Telegram.Dtos.Schema;
public record EewInformationSchema : Head
{
    [JsonPropertyName("body")]
    public required Body Body { get; init; }
}
