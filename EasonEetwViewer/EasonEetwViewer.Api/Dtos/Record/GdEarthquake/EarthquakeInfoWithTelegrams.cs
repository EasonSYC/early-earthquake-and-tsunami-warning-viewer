using System.Text.Json.Serialization;

namespace EasonEetwViewer.Api.Dtos.Record.GdEarthquake;
public record EarthquakeInfoWithTelegrams : EarthquakeInfo
{
    [JsonPropertyName("telegrams")]
    public required IEnumerable<TelegramItem> Telegrams { get; init; }
}
