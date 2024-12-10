using System.Text.Json.Serialization;
using EasonEetwViewer.HttpRequest.Dto.Enum;

namespace EasonEetwViewer.HttpRequest.Dto.Record;

/// <summary>
/// Represents a pair of coordinates describing a position.
/// </summary>
public record CoordinatePosition
{
    /// <summary>
    /// The property <c>latitude</c>. The latitude coordinate of the position.
    /// <c>null</c> when unclear.
    /// </summary>
    [JsonPropertyName("latitude")]
    public Coordinate? Latitude { get; init; }
    /// <summary>
    /// The property <c>longitude</c>. The longitude coordinate of the position.
    /// <c>null</c> when unclear.
    /// </summary>

    [JsonPropertyName("longitude")]
    public Coordinate? Longitude { get; init; }
    /// <summary>
    /// The property <c>height</c>. The height of the position.
    /// <c>null</c> when unclear or undefined.
    /// </summary>
    [JsonPropertyName("height")]
    public Height? Height { get; init; }
    /// <summary>
    /// The property <c>geodeticSystem</c>. The geodetic system of the coordinate.
    /// <c>null</c> depending on the situation.
    /// </summary>
    [JsonPropertyName("geodeticSystem")]
    public Geodetic? GeodeticSystem { get; init; }
    /// <summary>
    /// The property <c>condition</c>. Extra information provided for the coordinate.
    /// <c>null</c> when unnecessary.
    /// <c>不明</c> when coordinate is unclear.
    /// </summary>
    [JsonPropertyName("condition")]
    public string? Condition { get; init; }
}
