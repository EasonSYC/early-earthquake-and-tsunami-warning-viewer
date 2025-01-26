using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.EewInformation.Enum.Change;
[JsonConverter(typeof(JsonStringEnumConverter<MaxInt>))]
public enum MaxInt
{
    Unknown = 0,
    [JsonStringEnumMemberName("0")]
    NoChange = 1,
    [JsonStringEnumMemberName("1")]
    Increase = 2,
    [JsonStringEnumMemberName("2")]
    Decrease = 3
}
