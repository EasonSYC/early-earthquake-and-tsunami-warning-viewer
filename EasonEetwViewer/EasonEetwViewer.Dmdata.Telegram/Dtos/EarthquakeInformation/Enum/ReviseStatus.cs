using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Telegram.Dtos.EarthquakeInformation.Enum;

[JsonConverter(typeof(JsonStringEnumConverter<ReviseStatus>))]
public enum ReviseStatus
{
    [JsonStringEnumMemberName("上方修正")]
    Increase,
    [JsonStringEnumMemberName("追加")]
    Additional
}