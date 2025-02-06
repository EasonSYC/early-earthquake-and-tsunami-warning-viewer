using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.TsunamiInformation.Enum;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.TsunamiInformation;
public record TsunamiHeight
{
    [JsonPropertyName("type")]
    public string Type { get; } = "津波の高さ";
    [JsonPropertyName("unit")]
    public string Unit { get; } = "m";
    [JsonPropertyName("value")]
    public required double? Value { get; init; }
    [JsonPropertyName("over")]
    public bool? Over { get; init; }
    [JsonPropertyName("condition")]
    public HeightCondition? Condition { get; init; }
}
