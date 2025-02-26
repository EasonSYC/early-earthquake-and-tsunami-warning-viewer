using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.Dto.JsonTelegram.EarthquakeInformation;
public record Sva
{
    [JsonPropertyName("unit")]
    public string Unit { get; } = "cm/s";
    [JsonPropertyName("value")]
    public required float Value { get; init; }
}
