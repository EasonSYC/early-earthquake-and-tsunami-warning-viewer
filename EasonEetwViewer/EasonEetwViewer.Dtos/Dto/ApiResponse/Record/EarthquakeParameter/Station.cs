using System.Text.Json.Serialization;
using EasonEetwViewer.Dtos.Dto.ApiResponse.Enum.Station;

namespace EasonEetwViewer.Dtos.Dto.ApiResponse.Record.EarthquakeParameter;

/// <summary>
/// Represents an earthquake observation station.
/// </summary>
public record Station
{
    /// <summary>
    /// The property <c>region</c>, the region of the city.
    /// </summary>
    [JsonPropertyName("region")]
    public required StationPosition Region { get; init; }
    /// <summary>
    /// The property <c>city</c>, the city of the observation point.
    /// </summary>
    [JsonPropertyName("city")]
    public required StationPosition City { get; init; }
    /// <summary>
    /// The property <c>noCode</c>, the unique code assigned to the observation point.
    /// </summary>
    [JsonPropertyName("noCode")]
    public required string NoCode { get; init; }
    /// <summary>
    /// The property <c>code</c>, the code for the observation point in the XML.
    /// </summary>
    [JsonPropertyName("code")]
    public required string XmlCode { get; init; }
    /// <summary>
    /// The property <c>name</c>, the Kanji Name of the observation point.
    /// </summary>
    [JsonPropertyName("name")]
    public required string KanjiName { get; init; }
    /// <summary>
    /// The property <c>kana</c>, the Kana Name of the observation point.
    /// </summary>
    [JsonPropertyName("kana")]
    public required string KanaName { get; init; }
    /// <summary>
    /// The property <c>status</c>, the status of the observation point.
    /// </summary>
    [JsonPropertyName("status")]
    public required Status StationStatus { get; init; }
    /// <summary>
    /// The property <c>owner</c>, the owner of the observation point.
    /// </summary>
    [JsonPropertyName("owner")]
    public required string Owner { get; init; }
    /// <summary>
    /// The property <c>latitude</c>, the latitude of the observation point.
    /// </summary>
    [JsonPropertyName("latitude")]
    public required double Latitude { get; init; }
    /// <summary>
    /// The property <c>longitude</c>, the longitude of the observation point.
    /// </summary>
    [JsonPropertyName("longitude")]
    public required double Longitude { get; init; }
}
