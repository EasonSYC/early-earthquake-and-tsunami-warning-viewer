using System.Text.Json.Serialization;

namespace EasonEetwViewer.Telegram.Dtos.EewInformation.Enum;

[JsonConverter(typeof(JsonStringEnumConverter<HypocentrePosition>))]
public enum HypocentrePosition
{
    [JsonStringEnumMemberName("内陸")]
    Land,
    [JsonStringEnumMemberName("海域")]
    Sea
}
