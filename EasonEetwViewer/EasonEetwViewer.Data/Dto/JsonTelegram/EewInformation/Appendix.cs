using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EasonEetwViewer.HttpRequest.Dto.JsonTelegram.EewInformation.Enum.Change;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram.EewInformation;
public record Appendix
{
    [JsonPropertyName("maxIntChange")]
    public required MaxInt MaxIntensityChange { get; init; }
    [JsonPropertyName("maxLgIntChange")]
    public MaxInt? MaxLgIntensityChange { get; init; }
    [JsonPropertyName("maxIntChangeReason")]
    public required Reason MaxIntensityChangeReason { get; init; }
}
