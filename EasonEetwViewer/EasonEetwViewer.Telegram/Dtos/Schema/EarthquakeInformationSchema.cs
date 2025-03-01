using System.Text.Json.Serialization;
using EasonEetwViewer.Dtos.Dto.JsonTelegram.TelegramBase;
using EasonEetwViewer.Telegram.Dtos.EarthquakeInformation;

namespace EasonEetwViewer.Telegram.Dtos.Schema;
public record EarthquakeInformationSchema : Head
{
    [JsonPropertyName("body")]
    public required Body Body { get; init; }
}