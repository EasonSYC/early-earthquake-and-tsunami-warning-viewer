using System.Text.Json.Serialization;
using EasonEetwViewer.Dtos.ApiResponse.Record.GdEarthquake;
using EasonEetwViewer.Dtos.ApiResponse.ResponseBase;

namespace EasonEetwViewer.Dtos.ApiResponse.Response;
public record GdEarthquakeEvent : SuccessBase
{
    [JsonPropertyName("event")]
    public required EarthquakeInfoWithTelegrams EarthquakeEvent { get; init; }
}
