using System.Text.Json.Serialization;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram;
public record EarthquakeInformationObservationDataWithCondition : EarthquakeInformationObservationData
{
    [JsonPropertyName("condition")]
    public string? Condition { get; init; }
}
