using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.ResponseEnums;

[JsonConverter(typeof(JsonStringEnumConverter<TestStatus>))]
public enum TestStatus
{
    [JsonStringEnumMemberName("including")]
    Including,
    [JsonStringEnumMemberName("no")]
    No
}