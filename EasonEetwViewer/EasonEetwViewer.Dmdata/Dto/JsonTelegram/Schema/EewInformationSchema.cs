using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.EewInformation;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.TelegramBase;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.Schema;
public record EewInformationSchema : Head
{
    [JsonPropertyName("body")]
    public required Body Body { get; init; }
}
