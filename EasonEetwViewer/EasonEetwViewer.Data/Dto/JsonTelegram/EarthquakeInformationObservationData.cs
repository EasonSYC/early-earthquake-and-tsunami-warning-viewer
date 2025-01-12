using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EasonEetwViewer.HttpRequest.Dto.Enum;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram;

public record EarthquakeInformationObservationData
{
    [JsonPropertyName("code")]
    public required string Code { get; init; }
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    [JsonPropertyName("maxInt")]
    public Enum.EarthquakeIntensity? MaxInt { get; init; }
    [JsonPropertyName("revise")]
    public EarthquakeInformationReviseStatus? Revise { get; init; }
}
