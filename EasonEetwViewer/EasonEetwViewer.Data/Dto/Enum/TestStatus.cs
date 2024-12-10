using System.Text.Json.Serialization;

namespace EasonEetwViewer.HttpRequest.Dto.Enum;

/// <summary>
/// Represents whether test telegrams are received by a WebSocket connection.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<TestStatus>))]
public enum TestStatus
{
    /// <summary>
    /// Unknown. Default value.
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// The value <c>including</c>, representing receiving test telegrams.
    /// </summary>
    [JsonStringEnumMemberName("including")]
    Include = 1,
    /// <summary>
    /// The value <c>no</c>, representing not receiving test telegrams.
    /// </summary>
    [JsonStringEnumMemberName("no")]
    Exclude = 2
}