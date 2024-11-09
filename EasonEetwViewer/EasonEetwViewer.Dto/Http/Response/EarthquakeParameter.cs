using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EasonEetwViewer.Dto.Http.Response.Model;
using EasonEetwViewer.Dto.Http.Response.Record;

namespace EasonEetwViewer.Dto.Http.Response;

/// <summary>
/// Represents a response of an API call <c>parameter.earthquake</c>.
/// </summary>
public record EarthquakeParameter : ListResponse<EarthquakeStation>
{
    /// <summary>
    /// The property <c>changeTime</c>, representing the last time the list was changed.
    /// </summary>
    [JsonPropertyName("changeTime")]
    public DateTime ChangeTime { get; init; }
    /// <summary>
    /// The property <c>version</c>, a string indicating the version of the list.
    /// </summary>
    [JsonPropertyName("version")]
    public string Version { get; init; } = string.Empty;
}
