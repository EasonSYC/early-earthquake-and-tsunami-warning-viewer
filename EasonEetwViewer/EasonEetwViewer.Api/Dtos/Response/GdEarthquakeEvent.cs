using System.Text.Json.Serialization;
using EasonEetwViewer.Api.Dtos.Record.GdEarthquake;
using EasonEetwViewer.Api.Dtos.ResponseBase;

namespace EasonEetwViewer.Api.Dtos.Response;
public record GdEarthquakeEvent : SuccessBase
{
    [JsonPropertyName("event")]
    public required EarthquakeInfoWithTelegrams EarthquakeEvent { get; init; }
}
