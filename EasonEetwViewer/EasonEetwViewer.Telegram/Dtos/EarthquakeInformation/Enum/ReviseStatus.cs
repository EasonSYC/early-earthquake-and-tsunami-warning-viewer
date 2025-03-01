using System.Text.Json.Serialization;

namespace EasonEetwViewer.Telegram.Dtos.EarthquakeInformation.Enum;

[JsonConverter(typeof(JsonStringEnumConverter<ReviseStatus>))]
public enum ReviseStatus
{
    [JsonStringEnumMemberName("上方修正")]
    Increase,
    [JsonStringEnumMemberName("追加")]
    Additional
}