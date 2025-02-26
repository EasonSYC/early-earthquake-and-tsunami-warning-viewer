﻿using System.Text.Json.Serialization;
using EasonEetwViewer.Dtos.DmdataComponent;

namespace EasonEetwViewer.Dtos.Dto.JsonTelegram.EarthquakeInformation;
public record Body
{
    [JsonPropertyName("earthquake")]
    public EarthquakeComponent? Earthquake { get; init; }
    [JsonPropertyName("intensity")]
    public IntensityDetails? Intensity { get; init; }
    [JsonPropertyName("text")]
    public string? Text { get; init; }
    [JsonPropertyName("comments")]
    public Comments? Comments { get; init; }
}
