using System.Text.Json.Serialization;
using EasonEetwViewer.HttpRequest.Dto.Model;
using EasonEetwViewer.HttpRequest.Dto.Record;

namespace EasonEetwViewer.HttpRequest.Dto.Responses;

/// <summary>
/// Represents a response of an API call <c>parameter.earthquake</c>.
/// </summary>
public record EarthquakeParameterResponse : ListResponse<Station>
{
    /// <summary>
    /// The property <c>changeTime</c>, representing the last time the list was changed.
    /// </summary>
    [JsonPropertyName("changeTime")]
    public required DateTimeOffset ChangeTime { get; init; }
    /// <summary>
    /// The property <c>version</c>, a string indicating the version of the list.
    /// </summary>
    [JsonPropertyName("version")]
    public required string Version { get; init; }
}
