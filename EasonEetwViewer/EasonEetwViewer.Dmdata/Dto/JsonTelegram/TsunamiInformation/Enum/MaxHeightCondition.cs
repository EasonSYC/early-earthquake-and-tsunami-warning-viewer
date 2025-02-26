using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.TsunamiInformation.Enum;

[JsonConverter(typeof(JsonStringEnumConverter<MaxHeightCondition>))]
public enum MaxHeightCondition
{
    Unknown,
    [JsonStringEnumMemberName("重要")]
    Important
}
