using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.TelegramBase;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.TsunamiInformation;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.Schema;
public record TsunamiInformationSchema : Head
{
    [JsonPropertyName("body")]
    public required Body Body { get; init; }
}
