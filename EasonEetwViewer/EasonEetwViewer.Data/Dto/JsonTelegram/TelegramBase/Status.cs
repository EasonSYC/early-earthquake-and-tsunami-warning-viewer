using System.Text.Json.Serialization;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram.TelegramBase;

[JsonConverter(typeof(JsonStringEnumConverter<Status>))]
public enum Status
{
    Unknown = 0,
    [JsonStringEnumMemberName("通常")]
    Normal = 1,
    [JsonStringEnumMemberName("訓練")]
    Practise = 2,
    [JsonStringEnumMemberName("試験")]
    Test = 3
}
