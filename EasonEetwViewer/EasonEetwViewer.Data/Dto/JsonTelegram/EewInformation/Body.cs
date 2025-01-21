using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram.EewInformation;
public record Body
{
    [JsonPropertyName("isLastInfo")]
    public required bool IsLastInfo { get; init; }
    [JsonPropertyName("isCanceled")]
    public required bool IsCanceled { get; init; }
    [JsonPropertyName("isWarning")]
    public bool? IsWarning { get; init; }
    [JsonPropertyName("zones")]
    public List<Area>? Zones { get; init; }
    [JsonPropertyName("prefectures")]
    public List<Area>? Prefectures { get; init; }
    [JsonPropertyName("regions")]
    public List<Area>? Regions { get; init; }
    [JsonPropertyName("earthquake")]
    public Earthquake? Earthquake { get; init; }
    [JsonPropertyName("intensity")]
    public Intensity? Intensity { get; init; }
    [JsonPropertyName("text")]
    public string? Text { get; init; }
    [JsonPropertyName("comments")]
    public Comments? Comments { get; init; }
}
