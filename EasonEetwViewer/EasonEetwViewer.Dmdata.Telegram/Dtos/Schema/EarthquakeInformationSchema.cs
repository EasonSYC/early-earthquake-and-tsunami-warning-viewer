using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Telegram.Dtos.EarthquakeInformation;
using EasonEetwViewer.Dmdata.Telegram.Dtos.TelegramBase;

namespace EasonEetwViewer.Dmdata.Telegram.Dtos.Schema;
public record EarthquakeInformationSchema : Head
{
    [JsonPropertyName("body")]
    public required Body Body { get; init; }
}