using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.Dto.JsonTelegram.TsunamiInformation.Enum;
[JsonConverter(typeof(JsonStringEnumConverter<HeightCondition>))]
public enum HeightCondition
{
    [JsonStringEnumMemberName("高い")]
    High,
    [JsonStringEnumMemberName("巨大")]
    Huge
}
