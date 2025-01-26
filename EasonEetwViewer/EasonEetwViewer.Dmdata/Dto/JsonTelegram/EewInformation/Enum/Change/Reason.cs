using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.EewInformation.Enum.Change;
[JsonConverter(typeof(JsonStringEnumConverter<Reason>))]
public enum Reason
{
    Unknown = 0,
    [JsonStringEnumMemberName("0")]
    None = 1,
    [JsonStringEnumMemberName("1")]
    Magnitude = 2,
    [JsonStringEnumMemberName("2")]
    Position = 3,
    [JsonStringEnumMemberName("3")]
    MagnitudeAndPosition = 4,
    [JsonStringEnumMemberName("4")]
    Depth = 5,
    [JsonStringEnumMemberName("9")]
    Plum = 6
}
