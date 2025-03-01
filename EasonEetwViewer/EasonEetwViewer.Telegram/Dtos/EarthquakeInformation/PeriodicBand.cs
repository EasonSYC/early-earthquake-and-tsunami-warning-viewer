using System.Text.Json.Serialization;

namespace EasonEetwViewer.Telegram.Dtos.EarthquakeInformation;
public record PeriodicBand
{
    [JsonPropertyName("unit")]
    public string Unit { get; } = "秒台";
    [JsonPropertyName("value")]
    public required int Value { get; init; }
}
