using System.Text.Json.Serialization;
using EasonEetwViewer.Dtos.Dto.JsonTelegram.EewInformation;
using EasonEetwViewer.Dtos.Dto.JsonTelegram.TelegramBase;

namespace EasonEetwViewer.Dtos.Dto.JsonTelegram.Schema;
public record EewInformationSchema : Head
{
    [JsonPropertyName("body")]
    public required Body Body { get; init; }
}
