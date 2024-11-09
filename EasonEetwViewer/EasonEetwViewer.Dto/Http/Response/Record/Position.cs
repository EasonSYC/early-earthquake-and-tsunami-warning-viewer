using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EasonEetwViewer.Dto.Http.Response.Record;

/// <summary>
/// Represents a position (city or region).
/// </summary>
public record Position
{
    /// <summary>
    /// The <c>code</c> property. The unique code of the position.
    /// </summary>
    [JsonPropertyName("code")]
    public string Code { get; init; } = string.Empty;
    /// <summary>
    /// The <c>name</c> property. The Kanji name of the position.
    /// </summary>
    [JsonPropertyName("name")]
    public string KanjiName { get; init; } = string.Empty;
    /// <summary>
    /// The <c>kana</c> property. The Kana name of the position.
    /// </summary>
    [JsonPropertyName("kana")]
    public string KanaName { get; init; } = string.Empty;
}
