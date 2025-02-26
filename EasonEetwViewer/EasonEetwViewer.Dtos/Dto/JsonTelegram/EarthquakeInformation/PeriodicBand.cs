using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.Dto.JsonTelegram.EarthquakeInformation;
public record PeriodicBand
{
    [JsonPropertyName("unit")]
    public string Unit { get; } = "秒台";
    [JsonPropertyName("value")]
    public required int Value { get; init; }
}
