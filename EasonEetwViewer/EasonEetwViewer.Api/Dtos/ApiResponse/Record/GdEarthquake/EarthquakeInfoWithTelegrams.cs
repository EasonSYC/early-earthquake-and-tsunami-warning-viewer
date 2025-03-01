using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.ApiResponse.Record.GdEarthquake;
public record EarthquakeInfoWithTelegrams : EarthquakeInfo
{
    [JsonPropertyName("telegrams")]
    public required IEnumerable<Telegram> Telegrams { get; init; }
}
