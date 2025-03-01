using System.Text.Json.Serialization;
using EasonEetwViewer.Api.Dtos.ApiResponse.Record.GdEarthquake;
using EasonEetwViewer.Api.Dtos.ApiResponse.ResponseBase;

namespace EasonEetwViewer.Api.Dtos.ApiResponse.Response;
public record GdEarthquakeEvent : SuccessBase
{
    [JsonPropertyName("event")]
    public required EarthquakeInfoWithTelegrams EarthquakeEvent { get; init; }
}
