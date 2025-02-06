using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.TsunamiInformation.Enum;
[JsonConverter(typeof(JsonStringEnumConverter<Revise>))]
public enum Revise
{
    Unknown = 0,
    [JsonStringEnumMemberName("追加")]
    Add = 1,
    [JsonStringEnumMemberName("更新")]
    Renew = 2
}
