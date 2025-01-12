using System.Text.Json.Serialization;

namespace EasonEetwViewer.HttpRequest.Dto.Record;
public record EarthquakeInfoWithTelegrams : EarthquakeInfo
{
    [JsonPropertyName("telegrams")]
    public required List<EarthquakeTelegram> Telegrams { get; init; }
}
