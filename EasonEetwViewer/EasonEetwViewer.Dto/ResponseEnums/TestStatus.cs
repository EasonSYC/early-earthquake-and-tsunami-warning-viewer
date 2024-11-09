using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.ResponseEnums;

/// <summary>
/// Represents whether test telegrams are received by a WebSocket connection.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<TestStatus>))]
public enum TestStatus
{
    /// <summary>
    /// The <c>including</c> value, representing receiving test telegrams.
    /// </summary>
    [JsonStringEnumMemberName("including")]
    Include,
    /// <summary>
    /// The <c>no</c> value, representing not receiving test telegrams.
    /// </summary>
    [JsonStringEnumMemberName("no")]
    Exclude
}