using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.ApiResponse.Enum.WebSocket;

/// <summary>
/// Represents whether test telegrams are received by a WebSocket connection.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<TestStatus>))]
public enum TestStatus
{
    /// <summary>
    /// The value <c>including</c>, representing receiving test telegrams.
    /// </summary>
    [JsonStringEnumMemberName("including")]
    Include,
    /// <summary>
    /// The value <c>no</c>, representing not receiving test telegrams.
    /// </summary>
    [JsonStringEnumMemberName("no")]
    Exclude
}