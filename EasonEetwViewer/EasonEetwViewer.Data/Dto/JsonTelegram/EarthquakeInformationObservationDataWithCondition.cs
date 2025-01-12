using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram;
public record EarthquakeInformationObservationDataWithCondition : EarthquakeInformationObservationData
{
    [JsonPropertyName("condition")]
    public string? Condition { get; init; }
}
