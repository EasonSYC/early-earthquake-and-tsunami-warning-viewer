using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Telegram.Dtos.EewInformation.Enum.Change;
[JsonConverter(typeof(JsonStringEnumConverter<Reason>))]
public enum Reason
{
    [JsonStringEnumMemberName("0")]
    None = 0,
    [JsonStringEnumMemberName("1")]
    Magnitude = 1,
    [JsonStringEnumMemberName("2")]
    Position = 2,
    [JsonStringEnumMemberName("3")]
    MagnitudeAndPosition = 3,
    [JsonStringEnumMemberName("4")]
    Depth = 4,
    [JsonStringEnumMemberName("9")]
    Plum = 9
}
