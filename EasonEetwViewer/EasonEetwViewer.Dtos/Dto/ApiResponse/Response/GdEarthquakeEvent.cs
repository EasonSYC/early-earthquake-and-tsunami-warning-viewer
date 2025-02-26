using System.Text.Json.Serialization;
using EasonEetwViewer.Dtos.Dto.ApiResponse.Record.GdEarthquake;
using EasonEetwViewer.Dtos.Dto.ApiResponse.ResponseBase;

namespace EasonEetwViewer.Dtos.Dto.ApiResponse.Response;
public record GdEarthquakeEvent : SuccessBase
{
    [JsonPropertyName("event")]
    public required EarthquakeInfoWithTelegrams EarthquakeEvent { get; init; }
}
