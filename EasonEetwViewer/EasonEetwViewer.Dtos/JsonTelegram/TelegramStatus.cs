using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.JsonTelegram;

[JsonConverter(typeof(JsonStringEnumConverter<TelegramStatus>))]
public enum TelegramStatus
{
    [JsonStringEnumMemberName("通常")]
    Normal,
    [JsonStringEnumMemberName("訓練")]
    Practise,
    [JsonStringEnumMemberName("試験")]
    Test
}
