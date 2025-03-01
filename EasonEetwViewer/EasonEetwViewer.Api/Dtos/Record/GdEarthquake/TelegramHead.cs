using System.Text.Json.Serialization;

namespace EasonEetwViewer.Api.Dtos.Record.GdEarthquake;
/// <summary>
/// Represents the head data of a telegram, in <c>gd.earthquake.event</c>.
/// </summary>
public record TelegramHead
{
    /// <summary>
    /// The property <c>type</c>. The type of the telegram.
    /// </summary>
    [JsonPropertyName("type")]
    public required string Type { get; init; }
    /// <summary>
    /// The property <c>author</c>. The author of the telegram.
    /// </summary>
    [JsonPropertyName("author")]
    public required string Author { get; init; }
    /// <summary>
    /// The property <c>time</c>. The base time of the telegram.
    /// </summary>
    [JsonPropertyName("time")]
    public required DateTimeOffset Time { get; init; }
    /// <summary>
    /// The property <c>designation</c>. The designation code of the telegram.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when the telegram does not have a designation.
    /// </remarks>
    [JsonPropertyName("designation")]
    public required string? Designation { get; init; }
    /// <summary>
    /// The property <c>test</c>. Whether the telegram is a test telegram.
    /// </summary>
    /// <remarks>
    /// A constant <see langword="false"/> for this API call.
    /// </remarks>
    [JsonPropertyName("test")]
    public bool Test { get; } = false;
}
