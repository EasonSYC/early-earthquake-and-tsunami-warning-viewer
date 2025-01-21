using System.Text.Json.Serialization;
using EasonEetwViewer.HttpRequest.Dto.JsonTelegram.EewInformation;
using EasonEetwViewer.HttpRequest.Dto.JsonTelegram.TelegramBase;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram.Schema;
public record EewInformationSchema : Head
{
    [JsonPropertyName("body")]
    public required Body Body { get; init; }
}
