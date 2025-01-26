using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.EewInformation;
public record AreaKind
{
    [JsonPropertyName("code")]
    public string Code { get; } = "31";
    [JsonPropertyName("name")]
    public string Name { get; } = "緊急地震速報（警報）";
    [JsonPropertyName("lastKind")]
    public required SimpleKind LastKind { get; init; }
}
