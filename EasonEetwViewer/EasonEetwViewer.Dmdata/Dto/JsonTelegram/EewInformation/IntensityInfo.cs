using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.EewInformation.Enum.Range;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.EewInformation;
public record IntensityInfo
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
