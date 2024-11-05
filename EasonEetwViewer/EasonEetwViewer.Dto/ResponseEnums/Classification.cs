using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.ResponseEnums;

[JsonConverter(typeof(JsonStringEnumConverter<Classification>))]
public enum Classification
{
    [JsonStringEnumMemberName("telegram.earthquake")]
    TelegramEarthquake,
    [JsonStringEnumMemberName("telegram.volcano")]
    TelegramVolcano,
    [JsonStringEnumMemberName("telegram.weather")]
    TelegramWeather,
    [JsonStringEnumMemberName("telegram.forecast")]
    TelegramForecast,
    [JsonStringEnumMemberName("telegram.scheduled")]
    TelegramScheduled,
    [JsonStringEnumMemberName("eew.warning")]
    EewWarning,
    [JsonStringEnumMemberName("eew.forecast")]
    EewForecast,
    [JsonStringEnumMemberName("websocket.plus5")]
    WebSocketPlus5,
    [JsonStringEnumMemberName("websocket.plus2")]
    WebSocketPlus2
}