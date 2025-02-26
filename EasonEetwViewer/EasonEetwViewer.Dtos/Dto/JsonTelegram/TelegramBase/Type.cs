using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.Dto.JsonTelegram.TelegramBase;

[JsonConverter(typeof(JsonStringEnumConverter<Type>))]
public enum Type
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
