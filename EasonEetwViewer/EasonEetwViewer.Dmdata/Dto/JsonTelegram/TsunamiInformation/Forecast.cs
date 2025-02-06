using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.TsunamiInformation;
public record Forecast
{
    [JsonPropertyName("code")]
    public string Code { get; init; }
    [JsonPropertyName("name")]
    public string Name { get; init; }
    [JsonPropertyName("kind")]
    public required Kind Kind { get; init; }
    [JsonPropertyName("firstHeight")]
    public FirstHeight? FirstHeight { get; init; }
    [JsonPropertyName("maxHeight")]
    public MaxHeight? MaxHeight { get; init; }
    //[JsonPropertyName("stations")]
    //public IEnumerable<Station>? Stations { get; init; }
}
