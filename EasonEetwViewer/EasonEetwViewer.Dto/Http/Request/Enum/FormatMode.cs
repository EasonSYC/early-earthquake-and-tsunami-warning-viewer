using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.Http.Request.Enum;

/// <summary>
/// Represents the specified format mode of a WebSocket connection.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<FormatMode>))]
public enum FormatMode
{
    /// <summary>
    /// The value <c>raw</c>, representing raw formatting.
    /// </summary>
    [JsonStringEnumMemberName("raw")]
    Raw,
    /// <summary>
    /// The value <c>json</c>, representing formatting in JSON.
    /// </summary>
    [JsonStringEnumMemberName("json")]
    Json
}
