using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.Dto.JsonTelegram.TsunamiInformation;
public record Forecast
{
    [JsonPropertyName("code")]
    public required string Code { get; init; }
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    [JsonPropertyName("kind")]
    public required Kind Kind { get; init; }
    [JsonPropertyName("firstHeight")]
    public FirstHeight? FirstHeight { get; init; }
    [JsonPropertyName("maxHeight")]
    public MaxHeight? MaxHeight { get; init; }
    //[JsonPropertyName("stations")]
    //public IEnumerable<Station>? Stations { get; init; }
}
