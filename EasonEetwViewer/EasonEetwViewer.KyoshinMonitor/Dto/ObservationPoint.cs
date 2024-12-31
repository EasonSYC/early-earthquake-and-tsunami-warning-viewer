using System.Text.Json.Serialization;
using EasonEetwViewer.KyoshinMonitor.Dto.Enum;

namespace EasonEetwViewer.KyoshinMonitor.Dto;
public record ObservationPoint
{
    [JsonPropertyName("type")]
    [JsonInclude]
    public required PointType Type { get; init; }

    [JsonPropertyName("code")]
    [JsonInclude]
    public required string Code { get; init; }
    [JsonPropertyName("name")]
    [JsonInclude]
    public required string Name { get; init; }
    [JsonPropertyName("region")]
    [JsonInclude]
    public required string Region { get; init; }
    [JsonPropertyName("isSuspended")]
    [JsonInclude]
    public required bool IsSuspended { get; init; }
    [JsonPropertyName("location")]
    [JsonInclude]
    public required GeoCoordinate Location { get; init; }
    [JsonPropertyName("point")]
    [JsonInclude]
    public required Coordinate Point { get; init; }
}