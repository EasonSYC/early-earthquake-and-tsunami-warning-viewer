using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EasonEetwViewer.KyoshinMonitor.Dto;
public record Coordinate
{
    [JsonPropertyName("x")]
    [JsonInclude]
    public required int X { get; init; }
    [JsonPropertyName("y")]
    [JsonInclude]
    public required int Y { get; init; }
}
