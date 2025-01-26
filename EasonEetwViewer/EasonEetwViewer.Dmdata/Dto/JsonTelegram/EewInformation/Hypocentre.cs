using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.DmdataComponent;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.EewInformation;
public record Hypocentre
{
    [JsonPropertyName("code")]
    public required string Code { get; init; }
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    [JsonPropertyName("coordinate")]
    public required CoordinateComponent Coordinate { get; init; }
    [JsonPropertyName("depth")]
    public required DmdataComponent.Depth Depth { get; init; }
    [JsonPropertyName("reduce")]
    public required ReducedName ShortName { get; init; }
    [JsonPropertyName("landOrSea")]
    public Enum.Accuracy.Hypocentre? Position { get; init; }
    [JsonPropertyName("accuracy")]
    public required Accuracy Accuracy { get; init; }
}
