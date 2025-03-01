using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.Enum;

/// <summary>
/// Represents classifications of telegrams and subscriptions.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<Classification>))]
public enum Classification
{
    /// <summary>
    /// The value <c>telegram.earthquake</c>. Earthquake telegrams.
    /// </summary>
    [JsonStringEnumMemberName("telegram.earthquake")]
    TelegramEarthquake,
    /// <summary>
    /// The value <c>telegram.volcano</c>. Volcano telegrams.
    /// </summary>
    [JsonStringEnumMemberName("telegram.volcano")]
    TelegramVolcano,
    /// <summary>
    /// The value <c>telegram.weather</c>. Weather telegrams.
    /// </summary>
    [JsonStringEnumMemberName("telegram.weather")]
    TelegramWeather,
    /// <summary>
    /// The value <c>telegram.forecast</c>. Forecast telegrams.
    /// </summary>
    [JsonStringEnumMemberName("telegram.forecast")]
    TelegramForecast,
    /// <summary>
    /// The value <c>telegram.scheduled</c>. Scheduled telegrams.
    /// </summary>
    [JsonStringEnumMemberName("telegram.scheduled")]
    TelegramScheduled,
    /// <summary>
    /// The value <c>eew.warning</c>. EEW warnings.
    /// </summary>
    [JsonStringEnumMemberName("eew.warning")]
    EewWarning,
    /// <summary>
    /// The value <c>eew.forecast</c>. EEW forecasts.
    /// </summary>
    [JsonStringEnumMemberName("eew.forecast")]
    EewForecast,
    /// <summary>
    /// The value <c>websocket.plus5</c>. Add 5 WebSocket connections.
    /// </summary>
    [JsonStringEnumMemberName("websocket.plus5")]
    WebSocketPlus5,
    /// <summary>
    /// The value <c>websocket.plus2</c>. Add 2 WebSocket connections.
    /// </summary>
    [JsonStringEnumMemberName("websocket.plus2")]
    WebSocketPlus2
}