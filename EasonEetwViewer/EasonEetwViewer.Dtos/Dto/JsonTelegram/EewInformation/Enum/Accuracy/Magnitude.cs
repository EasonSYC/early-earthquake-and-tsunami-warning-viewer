using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.Dto.JsonTelegram.EewInformation.Enum.Accuracy;
[JsonConverter(typeof(JsonStringEnumConverter<Magnitude>))]
public enum Magnitude
{
    [JsonStringEnumMemberName("0")]
    Unknown = 0,
    [JsonStringEnumMemberName("2")]
    SpeedMagnitude = 2,
    [JsonStringEnumMemberName("3")]
    FullPPhase = 3,
    [JsonStringEnumMemberName("4")]
    FullPPhaseMixed = 4,
    [JsonStringEnumMemberName("5")]
    FullPointPhase = 5,
    [JsonStringEnumMemberName("6")]
    Epos = 6,
    [JsonStringEnumMemberName("8")]
    LevelOrPlum = 8
}
