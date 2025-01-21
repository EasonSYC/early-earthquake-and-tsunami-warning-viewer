using System.Text.Json.Serialization;
using EasonEetwViewer.HttpRequest.Dto.JsonTelegram.TelegramBase;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram.EewInformation;
public record Comments
{
    [JsonPropertyName("free")]
    public string? FreeText { get; init; }
    [JsonPropertyName("warning")]
    public AdditionalComment? Forecast { get; init; }
}
