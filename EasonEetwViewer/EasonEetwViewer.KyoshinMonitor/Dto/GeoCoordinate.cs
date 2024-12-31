using System.Text.Json.Serialization;

namespace EasonEetwViewer.KyoshinMonitor.Dto;
public record GeoCoordinate
{
    [JsonPropertyName("latitude")]
    [JsonInclude]
    public required double Latitude { get; init; }
    [JsonPropertyName("longitude")]
    [JsonInclude]
    public required double Longitude { get; init; }
}
