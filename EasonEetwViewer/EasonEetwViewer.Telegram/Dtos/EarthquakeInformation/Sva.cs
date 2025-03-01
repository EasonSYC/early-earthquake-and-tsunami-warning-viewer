using System.Text.Json.Serialization;

namespace EasonEetwViewer.Telegram.Dtos.EarthquakeInformation;
public record Sva
{
    [JsonPropertyName("unit")]
    public string Unit { get; } = "cm/s";
    [JsonPropertyName("value")]
    public required float Value { get; init; }
}
