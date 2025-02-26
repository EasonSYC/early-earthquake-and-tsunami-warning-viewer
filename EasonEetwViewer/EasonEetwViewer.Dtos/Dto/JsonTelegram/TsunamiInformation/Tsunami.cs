﻿using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.Dto.JsonTelegram.TsunamiInformation;
public record Tsunami
{
    [JsonPropertyName("forecasts")]
    public IEnumerable<Forecast>? Forecasts { get; init; }
    //[JsonPropertyName("observations")]
    //public IEnumerable<Observation>? Observations { get; init; }
    //[JsonPropertyName("estimations")]
    //public IEnumerable<Estimation>? Estimations { get; init; }
}
