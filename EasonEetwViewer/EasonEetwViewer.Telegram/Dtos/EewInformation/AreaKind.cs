using System.Text.Json.Serialization;

namespace EasonEetwViewer.Telegram.Dtos.EewInformation;
public record AreaKind
{
    [JsonPropertyName("code")]
    public string Code { get; } = "31";
    [JsonPropertyName("name")]
    public string Name { get; } = "緊急地震速報（警報）";
    [JsonPropertyName("lastKind")]
    public required SimpleKind LastKind { get; init; }
}
