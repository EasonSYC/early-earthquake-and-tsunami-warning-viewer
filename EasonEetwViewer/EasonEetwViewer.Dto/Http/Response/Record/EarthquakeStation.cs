using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EasonEetwViewer.Dto.Http.Response.Enum;

namespace EasonEetwViewer.Dto.Http.Response.Record;

/// <summary>
/// Represents an earthquake observation station.
/// </summary>
public record EarthquakeStation
{
    /// <summary>
    /// The property <c>region</c>, the region of the city.
    /// </summary>
    [JsonPropertyName("region")]
    public Position Region { get; init; } = new();
    /// <summary>
    /// The property <c>city</c>, the city of the observation point.
    /// </summary>
    [JsonPropertyName("city")]
    public Position City { get; init; } = new();
    /// <summary>
    /// The property <c>noCode</c>, the unique code assigned to the observation point.
    /// </summary>
    [JsonPropertyName("noCode")]
    public string NoCode { get; init; } = string.Empty;
    /// <summary>
    /// The property <c>code</c>, the code for the observation point in the XML.
    /// </summary>
    [JsonPropertyName("code")]
    public string XmlCode { get; init; } = string.Empty;
    /// <summary>
    /// The property <c>name</c>, the Kanji Name of the observation point.
    /// </summary>
    [JsonPropertyName("name")]
    public string KanjiName { get; init; } = string.Empty;
    /// <summary>
    /// The property <c>kana</c>, the Kana Name of the observation point.
    /// </summary>
    [JsonPropertyName("kana")]
    public string KanaName { get; init; } = string.Empty;
    /// <summary>
    /// The property <c>status</c>, the status of the observation point.
    /// </summary>
    [JsonPropertyName("status")]
    public StationStatus StationStatus { get; init; }
    /// <summary>
    /// The property <c>owner</c>, the owner of the observation point.
    /// </summary>
    [JsonPropertyName("owner")]
    public string Owner { get; init; } = string.Empty;
    /// <summary>
    /// The property <c>latitude</c>, the latitude of the observation point.
    /// </summary>
    [JsonPropertyName("latitude")]
    public string Latitude { get; init; } = string.Empty;
    /// <summary>
    /// The property <c>longitude</c>, the longitude of the observation point.
    /// </summary>
    [JsonPropertyName("longitude")]
    public string Longitude { get; init; } = string.Empty;
}
