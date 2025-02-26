using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.EarthquakeInformation;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.TelegramBase;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.Schema;
public record EarthquakeInformationSchema : Head
{
    [JsonPropertyName("body")]
    public required Body Body { get; init; }
}