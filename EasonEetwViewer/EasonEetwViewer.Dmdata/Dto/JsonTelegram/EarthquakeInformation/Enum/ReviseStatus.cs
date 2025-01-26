using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.EarthquakeInformation.Enum;

[JsonConverter(typeof(JsonStringEnumConverter<ReviseStatus>))]
public enum ReviseStatus
{
    Unknown = 0,
    [JsonStringEnumMemberName("上方修正")]
    Increase = 1,
    [JsonStringEnumMemberName("追加")]
    Additional = 2
}