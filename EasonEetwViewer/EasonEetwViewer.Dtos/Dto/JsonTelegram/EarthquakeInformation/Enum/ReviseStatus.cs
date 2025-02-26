using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.Dto.JsonTelegram.EarthquakeInformation.Enum;

[JsonConverter(typeof(JsonStringEnumConverter<ReviseStatus>))]
public enum ReviseStatus
{
    [JsonStringEnumMemberName("上方修正")]
    Increase,
    [JsonStringEnumMemberName("追加")]
    Additional
}