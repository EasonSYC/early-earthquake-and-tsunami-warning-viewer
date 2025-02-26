using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.Dto.JsonTelegram.TsunamiInformation.Enum;
[JsonConverter(typeof(JsonStringEnumConverter<Revise>))]
public enum Revise
{
    [JsonStringEnumMemberName("追加")]
    Add,
    [JsonStringEnumMemberName("更新")]
    Renew
}
