using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Api.Dtos.Record.GdEarthquake;
/// <summary>
/// Represents an earthquake in the API call <c>gd.earthquake.event</c> which includes the telegrams.
/// </summary>
public record EarthquakeInfoWithTelegrams : EarthquakeInfo
{
    /// <summary>
    /// The property <c>telegrams</c>. The telegrams related to the earthquake.
    /// </summary>
    [JsonPropertyName("telegrams")]
    public required IEnumerable<TelegramItem> Telegrams { get; init; }
}
