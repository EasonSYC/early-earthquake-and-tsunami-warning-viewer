using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EasonEetwViewer.HttpRequest.Dto.Record;
public record EarthquakeTelegramHead
{
    [JsonPropertyName("type")]
    public required string Type { get; init; }
    [JsonPropertyName("author")]
    public required string Author { get; init; }
    [JsonPropertyName("time")]
    public required DateTime Time { get; init; }
    [JsonPropertyName("designation")]
    public required string? Designation { get; init; }
    [JsonPropertyName("test")]
    public bool Test { get; } = false;
}
