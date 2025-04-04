﻿using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Telegram.Dtos.EewInformation.Enum.Range;

namespace EasonEetwViewer.Dmdata.Telegram.Dtos.EewInformation;
public record IntensityInfo
{
    [JsonPropertyName("forecastMaxInt")]
    public required FromTo<IntensityLower, IntensityUpper> ForecastMaxInt { get; init; }
    [JsonPropertyName("forecastMaxLgInt")]
    public FromTo<LgIntensityLower, LgIntensityUpper>? ForecastMaxLgInt { get; init; }
    [JsonPropertyName("appendix")]
    public Appendix? Appendix { get; init; }
    [JsonPropertyName("regions")]
    public required IEnumerable<Region> Regions { get; init; }
    [JsonPropertyName("stations")]
    public IEnumerable<Station>? Stations { get; init; }
}
