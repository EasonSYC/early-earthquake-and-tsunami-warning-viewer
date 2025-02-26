using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.Dto.JsonTelegram.EewInformation.Enum;

[JsonConverter(typeof(JsonStringEnumConverter<HypocentrePosition>))]
public enum HypocentrePosition
{
    [JsonStringEnumMemberName("内陸")]
    Land,
    [JsonStringEnumMemberName("海域")]
    Sea
}
