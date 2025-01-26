using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.EewInformation.Enum.Accuracy;

[JsonConverter(typeof(JsonStringEnumConverter<Hypocentre>))]
public enum Hypocentre
{
    Unknown = 0,
    [JsonStringEnumMemberName("内陸")]
    Land = 1,
    [JsonStringEnumMemberName("海域")]
    Sea = 2
}
