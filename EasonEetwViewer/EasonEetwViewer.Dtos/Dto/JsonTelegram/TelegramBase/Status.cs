using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.TelegramBase;

[JsonConverter(typeof(JsonStringEnumConverter<Status>))]
public enum Status
{
    [JsonStringEnumMemberName("通常")]
    Normal,
    [JsonStringEnumMemberName("訓練")]
    Practise,
    [JsonStringEnumMemberName("試験")]
    Test
}
