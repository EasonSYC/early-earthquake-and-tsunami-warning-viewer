using System.Text.Json.Serialization;

namespace EasonEetwViewer.HttpRequest.Dto.Record;

/// <summary>
/// Represents a position (city or region).
/// </summary>
public record Position
{
    /// <summary>
    /// The <c>code</c> property. The unique code of the position.
    /// </summary>
    [JsonPropertyName("code")]
    public required string Code { get; init; }
    /// <summary>
    /// The <c>name</c> property. The Kanji name of the position.
    /// </summary>
    [JsonPropertyName("name")]
    public required string KanjiName { get; init; }
    /// <summary>
    /// The <c>kana</c> property. The Kana name of the position.
    /// </summary>
    [JsonPropertyName("kana")]
    public required string KanaName { get; init; }
}
