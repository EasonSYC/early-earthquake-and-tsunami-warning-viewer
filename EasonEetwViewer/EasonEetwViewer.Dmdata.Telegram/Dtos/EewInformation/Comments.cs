using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Telegram.Dtos.TelegramBase;

namespace EasonEetwViewer.Dmdata.Telegram.Dtos.EewInformation;
public record Comments
{
    [JsonPropertyName("free")]
    public string? FreeText { get; init; }
    [JsonPropertyName("warning")]
    public AdditionalComment? Warning { get; init; }
}
