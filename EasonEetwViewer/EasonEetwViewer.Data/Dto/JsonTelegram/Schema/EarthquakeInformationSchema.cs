using System.Text.Json.Serialization;
using EasonEetwViewer.HttpRequest.Dto.JsonTelegram.EarthquakeInformation;
using EasonEetwViewer.HttpRequest.Dto.JsonTelegram.TelegramBase;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram.Schema;
public record EarthquakeInformationSchema : Head
{
    [JsonPropertyName("body")]
    public required Body Body { get; init; }
}