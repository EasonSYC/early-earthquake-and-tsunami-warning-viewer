using System.Text.Json.Serialization;

namespace EasonEetwViewer.Telegram.Dtos.EewInformation.Enum.Change;
[JsonConverter(typeof(JsonStringEnumConverter<MaxInt>))]
public enum MaxInt
{
    [JsonStringEnumMemberName("0")]
    NoChange = 0,
    [JsonStringEnumMemberName("1")]
    Increase = 1,
    [JsonStringEnumMemberName("2")]
    Decrease = 2
}
