using System.Text.Json.Serialization;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram;
public record EarthquakeInformationSchema : JsonSchemaHead
{
    [JsonPropertyName("body")]
    public required EarthquakeInformationBody Body { get; init; }
}