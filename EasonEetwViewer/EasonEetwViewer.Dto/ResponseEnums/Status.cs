using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.ResponseEnums;

[JsonConverter(typeof(JsonStringEnumConverter<Status>))]
public enum Status
{
    [JsonStringEnumMemberName("ok")]
    Ok,
    [JsonStringEnumMemberName("error")]
    Error
}