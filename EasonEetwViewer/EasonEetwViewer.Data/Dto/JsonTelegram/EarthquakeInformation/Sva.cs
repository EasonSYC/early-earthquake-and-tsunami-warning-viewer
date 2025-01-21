using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram.EarthquakeInformation;
public record Sva
{
    [JsonPropertyName("unit")]
    public string Unit { get; } = "cm/s";
    [JsonPropertyName("value")]
    public required float Value { get; init; }
}
