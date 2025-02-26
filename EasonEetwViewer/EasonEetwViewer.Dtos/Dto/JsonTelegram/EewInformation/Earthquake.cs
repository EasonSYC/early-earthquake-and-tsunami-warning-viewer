using System.Text.Json.Serialization;
using EasonEetwViewer.Dtos.DmdataComponent;

namespace EasonEetwViewer.Dtos.Dto.JsonTelegram.EewInformation;
public record Earthquake
{
    [JsonPropertyName("originTime")]
    public DateTimeOffset? OriginTime { get; init; }
    [JsonPropertyName("arrivalTime")]
    public required DateTimeOffset ArrivalTime { get; init; }
    [JsonPropertyName("condition")]
    public string? Condition { get; init; }
    [JsonPropertyName("hypocenter")]
    public required Hypocentre Hypocentre { get; init; }
    [JsonPropertyName("magnitude")]
    public required Magnitude Magnitude { get; init; }
}
