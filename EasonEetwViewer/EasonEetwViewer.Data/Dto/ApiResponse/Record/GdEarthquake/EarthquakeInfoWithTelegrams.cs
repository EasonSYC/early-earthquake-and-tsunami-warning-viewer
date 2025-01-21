using System.Text.Json.Serialization;

namespace EasonEetwViewer.HttpRequest.Dto.ApiResponse.Record.GdEarthquake;
public record EarthquakeInfoWithTelegrams : EarthquakeInfo
{
    [JsonPropertyName("telegrams")]
    public required List<Telegram> Telegrams { get; init; }
}
