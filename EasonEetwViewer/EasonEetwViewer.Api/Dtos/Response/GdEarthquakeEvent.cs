using System.Text.Json.Serialization;
using EasonEetwViewer.Api.Dtos.Record.GdEarthquake;
using EasonEetwViewer.Api.Dtos.ResponseBase;

namespace EasonEetwViewer.Api.Dtos.Response;
/// <summary>
/// Represents the result of an API call on <c>gd.earthquake.event</c> API.
/// </summary>
public record GdEarthquakeEvent : SuccessBase
{
    /// <summary>
    /// The property <c>event</c>, the earthquake event.
    /// </summary>
    [JsonPropertyName("event")]
    public required EarthquakeInfoWithTelegrams EarthquakeEvent { get; init; }
}
