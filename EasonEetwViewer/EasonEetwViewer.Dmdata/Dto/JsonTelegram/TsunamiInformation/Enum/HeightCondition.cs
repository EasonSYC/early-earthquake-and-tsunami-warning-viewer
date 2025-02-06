using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.TsunamiInformation.Enum;
[JsonConverter(typeof(JsonStringEnumConverter<HeightCondition>))]
public enum HeightCondition
{
    Unknown = 0,
    [JsonStringEnumMemberName("高い")]
    High = 1,
    [JsonStringEnumMemberName("巨大")]
    Huge = 2
}
