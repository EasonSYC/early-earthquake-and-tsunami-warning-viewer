using System.Text.Json.Serialization;
using EasonEetwViewer.Dtos.Dto.ApiResponse.Record.EarthquakeParameter;
using EasonEetwViewer.Dtos.Dto.ApiResponse.ResponseBase;

namespace EasonEetwViewer.Dtos.Dto.ApiResponse.Response;

/// <summary>
/// Represents a response of an API call <c>parameter.earthquake</c>.
/// </summary>
public record EarthquakeParameter : ListBase<Station>
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
