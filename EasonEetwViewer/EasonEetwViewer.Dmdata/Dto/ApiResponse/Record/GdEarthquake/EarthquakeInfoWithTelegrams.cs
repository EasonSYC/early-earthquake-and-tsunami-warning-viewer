using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dto.ApiResponse.Record.GdEarthquake;
public record EarthquakeInfoWithTelegrams : EarthquakeInfo
{
    [JsonPropertyName("telegrams")]
    public required IEnumerable<Telegram> Telegrams { get; init; }
}
