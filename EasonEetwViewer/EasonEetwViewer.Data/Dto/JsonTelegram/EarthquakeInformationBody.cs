using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EasonEetwViewer.HttpRequest.DmdataComponent;
using EasonEetwViewer.HttpRequest.Dto.Enum;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram;
public record EarthquakeInformationBody
{
    [JsonPropertyName("earthquake")]
    public EarthquakeComponent? Earthquake { get; init; }
    [JsonPropertyName("intensity")]
    public EarthquakeInformationIntensity? Intensity { get; init; }
    [JsonPropertyName("text")]
    public string? Text { get; init; }
    [JsonPropertyName("comments")]
    public EarthquakeInformationComments? Comments { get; init; }
}
