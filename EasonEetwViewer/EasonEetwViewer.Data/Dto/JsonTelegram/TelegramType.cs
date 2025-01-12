using System.Text.Json.Serialization;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram;

[JsonConverter(typeof(JsonStringEnumConverter<TelegramType>))]
public enum TelegramType
{
    Unknown = 0,
    [JsonStringEnumMemberName("発表")]
    Release = 1,
    [JsonStringEnumMemberName("訂正")]
    Correction = 2,
    [JsonStringEnumMemberName("遅延")]
    Delay = 3,
    [JsonStringEnumMemberName("取消")]
    Cancel = 4
}
