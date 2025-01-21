using System.Text.Json.Serialization;
using EasonEetwViewer.HttpRequest.Dto.ApiResponse.ResponseBase;
using EasonEetwViewer.HttpRequest.Dto.Record;

namespace EasonEetwViewer.HttpRequest.Dto.ApiResponse.Response;
public record GdEarthquakeEvent : SuccessBase
{
    [JsonPropertyName("event")]
    public required EarthquakeInfoWithTelegrams EarthquakeEvent { get; init; }
}
