using System.Text.Json.Serialization;

namespace EasonEetwViewer.HttpRequest.Dto.Record;

/// <summary>
/// Provides details for the hypocentre area name.
/// </summary>
public record HypocentreDetail
{
    /// <summary>
    /// The property <c>code</c>. The XML code for the area.
    /// </summary>
    [JsonPropertyName("code")]
    public required int Code { get; init; }
    /// <summary>
    /// The property <c>name</c>. The name for the area.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }
}
