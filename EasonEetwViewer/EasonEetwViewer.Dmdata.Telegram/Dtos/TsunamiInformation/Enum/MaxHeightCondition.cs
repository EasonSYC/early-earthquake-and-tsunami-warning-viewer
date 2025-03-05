using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Telegram.Dtos.TsunamiInformation.Enum;

[JsonConverter(typeof(JsonStringEnumConverter<MaxHeightCondition>))]
public enum MaxHeightCondition
{
    [JsonStringEnumMemberName("重要")]
    Important
}
