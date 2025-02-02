using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.EewInformation.Enum.Accuracy;
[JsonConverter(typeof(JsonStringEnumConverter<MagnitudePoint>))]
public enum MagnitudePoint
{
    [JsonStringEnumMemberName("0")]
    Unknown = 0,
    [JsonStringEnumMemberName("1")]
    OneOrLevelOrPlum = 1,
    [JsonStringEnumMemberName("2")]
    Two = 2,
    [JsonStringEnumMemberName("3")]
    Three = 3,
    [JsonStringEnumMemberName("4")]
    Four = 4,
    [JsonStringEnumMemberName("5")]
    FiveOrAbove = 5
}
