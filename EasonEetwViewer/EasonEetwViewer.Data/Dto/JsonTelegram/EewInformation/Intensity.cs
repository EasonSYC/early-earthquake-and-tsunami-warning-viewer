using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EasonEetwViewer.HttpRequest.Dto.JsonTelegram.EewInformation.Enum.Range;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram.EewInformation;
public record Intensity
{
    [JsonPropertyName("forecastMaxInt")]
    public required FromTo<IntensityLower, IntensityUpper> ForecastMaxInt { get; init; }
    [JsonPropertyName("forecastMaxLgInt")]
    public FromTo<LgIntensityLower, LgIntensityUpper>? ForecastMaxLgInt { get; init; }
    [JsonPropertyName("appendix")]
    public Appendix? Appendix { get; init; }
    [JsonPropertyName("regions")]
    public required List<Region> Regions { get; init; }
    [JsonPropertyName("stations")]
    public required List<Station> Stations { get; init; }
}
