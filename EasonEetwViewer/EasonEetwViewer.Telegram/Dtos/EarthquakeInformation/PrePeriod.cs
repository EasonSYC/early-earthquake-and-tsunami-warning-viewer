using System.Text.Json.Serialization;
using EasonEetwViewer.Dtos.Dto.ApiResponse.Enum;
using EasonEetwViewer.Telegram.Dtos.EarthquakeInformation;

namespace EasonEetwViewer.Telegram.Dtos.EarthquakeInformation;
public record PrePeriod
{
    [JsonPropertyName("periodicBand")]
    public required PeriodicBand Band { get; init; }
    [JsonPropertyName("lgInt")]
    public required LgIntensity LgInt { get; init; }
    [JsonPropertyName("sva")]
    public required Sva Sva { get; init; }
}
