using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
