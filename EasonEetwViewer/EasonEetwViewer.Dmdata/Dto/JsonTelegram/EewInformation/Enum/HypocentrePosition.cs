using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.EewInformation.Enum;

[JsonConverter(typeof(JsonStringEnumConverter<HypocentrePosition>))]
public enum HypocentrePosition
{
    Unknown = 0,
    [JsonStringEnumMemberName("内陸")]
    Land = 1,
    [JsonStringEnumMemberName("海域")]
    Sea = 2
}
