using System.Text.Json.Serialization;
using EasonEetwViewer.Dtos.Dto.JsonTelegram.EarthquakeInformation;
using EasonEetwViewer.Dtos.Dto.JsonTelegram.TelegramBase;

namespace EasonEetwViewer.Dtos.Dto.JsonTelegram.Schema;
public record EarthquakeInformationSchema : Head
{
    [JsonPropertyName("body")]
    public required Body Body { get; init; }
}