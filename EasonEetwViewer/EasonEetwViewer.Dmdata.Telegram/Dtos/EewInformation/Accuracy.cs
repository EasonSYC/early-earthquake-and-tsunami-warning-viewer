using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Telegram.Dtos.EewInformation.Enum.Accuracy;

namespace EasonEetwViewer.Dmdata.Telegram.Dtos.EewInformation;
public record Accuracy
{
    [JsonPropertyName("epicenters")]
    public required EpicentreDepth[] Epicentres { get; init; }
    [JsonPropertyName("depth")]
    public required EpicentreDepth Depth { get; init; }
    [JsonPropertyName("magnitudeCalculation")]
    public required Magnitude Magnitude { get; init; }
    [JsonPropertyName("numberOfMagnitudeCalculation")]
    public required MagnitudePoint MagnitudePoint { get; init; }
}
