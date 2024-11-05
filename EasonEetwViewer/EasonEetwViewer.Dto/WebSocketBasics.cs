using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto;

public abstract record WebSocketBasics
{
    [JsonPropertyName("ticket")]
    public string Ticket { get; init; } = string.Empty;
    [JsonPropertyName("classifications")]
    public List<ResponseEnums.Classification> Classifications { get; init; } = [];
    [JsonPropertyName("test")]
    public ResponseEnums.TestStatus Test { get; init; }
    [JsonPropertyName("types")]
    public List<ResponseEnums.TelegramType>? Types { get; init; }
    [JsonPropertyName("formats")]
    public List<ResponseEnums.FormatType> Formats { get; init; } = [];
    [JsonPropertyName("appName")]
    public string? AppName { get; init; }
}