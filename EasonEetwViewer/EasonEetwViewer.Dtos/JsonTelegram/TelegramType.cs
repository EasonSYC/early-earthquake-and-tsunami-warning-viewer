using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.JsonTelegram;

[JsonConverter(typeof(JsonStringEnumConverter<TelegramType>))]
public enum TelegramType
{
    [JsonStringEnumMemberName("発表")]
    Release,
    [JsonStringEnumMemberName("訂正")]
    Correction,
    [JsonStringEnumMemberName("遅延")]
    Delay,
    [JsonStringEnumMemberName("取消")]
    Cancel
}
