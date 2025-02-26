using System.Text.Json.Serialization;
using EasonEetwViewer.Dtos.Dto.JsonTelegram.TelegramBase;
using EasonEetwViewer.Dtos.Dto.JsonTelegram.TsunamiInformation;

namespace EasonEetwViewer.Dtos.Dto.JsonTelegram.Schema;
public record TsunamiInformationSchema : Head
{
    [JsonPropertyName("body")]
    public required Body Body { get; init; }
}
