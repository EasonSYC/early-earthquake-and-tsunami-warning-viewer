using System.Text.Json.Serialization;

namespace EasonEetwViewer.WebSocket.Dtos.Data;
/// <summary>
/// Represents a passing detail of the specified data response.
/// </summary>
internal record PassingDetail
{
    /// <summary>
    /// The property <c>name</c>, the name of the passing.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    /// <summary>
    /// The property <c>time</c>, the time of the passing.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("time")]
    public required DateTimeOffset Time { get; init; }
}
