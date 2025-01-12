using System.Text.Json.Serialization;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram;

[JsonConverter(typeof(JsonStringEnumConverter<EarthquakeInformationReviseStatus>))]
public enum EarthquakeInformationReviseStatus
{
    Unknown = 0,
    [JsonStringEnumMemberName("上方修正")]
    Increase = 1,
    [JsonStringEnumMemberName("追加")]
    Additional = 2
}