using System.Text.Json.Serialization;
using EasonEetwViewer.HttpRequest.Dto.JsonTelegram.EewInformation.Enum.Accuracy;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram.EewInformation;
public record Accuracy
{
    [JsonPropertyName("epicenters")]
    public required List<Epicentre> Epicentres { get; init; }
    [JsonPropertyName("depth")]
    public required Depth Depth { get; init; }
    [JsonPropertyName("magnitudeCalculation")]
    public required Magnitude Magnitude { get; init; }
    [JsonPropertyName("numberOfMagnitudeCalculation")]
    public required MagnitudePoint MagnitudePoint { get; init; }
}
