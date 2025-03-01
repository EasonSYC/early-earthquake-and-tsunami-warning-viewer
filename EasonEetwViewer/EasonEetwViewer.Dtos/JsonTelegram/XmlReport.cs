using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.JsonTelegram;
public class XmlReport
{
    [JsonPropertyName("head")]
    public required XmlHead Head { get; init; }
    [JsonPropertyName("control")]
    public required XmlControl Control { get; init; }
}
