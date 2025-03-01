using System.Text.Json.Serialization;
using EasonEetwViewer.Api.Dtos.ApiResponse.Record.GdEarthquake;

namespace EasonEetwViewer.Api.Dtos.ApiResponse.Record.GdEarthquake;
public record EarthquakeInfoWithTelegrams : EarthquakeInfo
{
    [JsonPropertyName("telegrams")]
    public required IEnumerable<TelegramItem> Telegrams { get; init; }
}
