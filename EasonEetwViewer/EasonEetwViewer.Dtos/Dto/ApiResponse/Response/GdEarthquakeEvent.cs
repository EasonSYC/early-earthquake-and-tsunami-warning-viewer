using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Record.GdEarthquake;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.ResponseBase;

namespace EasonEetwViewer.Dmdata.Dto.ApiResponse.Response;
public record GdEarthquakeEvent : SuccessBase
{
    [JsonPropertyName("event")]
    public required EarthquakeInfoWithTelegrams EarthquakeEvent { get; init; }
}
