using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.ResponseEnums;

[JsonConverter(typeof(JsonStringEnumConverter<FormatType>))]
public enum FormatType
{
    [JsonStringEnumMemberName("xml")]
    Xml,
    [JsonStringEnumMemberName("json")]
    Json,
    [JsonStringEnumMemberName("a/n")]
    An,
    [JsonStringEnumMemberName("binary")]
    Binary
}