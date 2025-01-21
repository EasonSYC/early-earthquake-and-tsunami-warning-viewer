using System.Text.Json.Serialization;
using EasonEetwViewer.HttpRequest.Dto.ApiResponse.Record.GdEarthquake;
using EasonEetwViewer.HttpRequest.Dto.ApiResponse.ResponseBase;

namespace EasonEetwViewer.HttpRequest.Dto.ApiResponse.Response;
public record GdEarthquakeEvent : SuccessBase
{
    [JsonPropertyName("event")]
    public required EarthquakeInfoWithTelegrams EarthquakeEvent { get; init; }
}
