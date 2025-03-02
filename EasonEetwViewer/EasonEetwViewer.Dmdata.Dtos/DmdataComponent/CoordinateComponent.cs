using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Dtos.DmdataComponent.Enum;

namespace EasonEetwViewer.Dmdata.Dtos.DmdataComponent;

/// <summary>
/// Represents a pair of coordinates describing a position.
/// </summary>
public record CoordinateComponent
{
    /// <summary>
    /// The property <c>latitude</c>. The latitude coordinate of the position.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when unclear.
    /// </remarks>
    [JsonPropertyName("latitude")]
    public Coordinate? Latitude { get; init; }
    /// <summary>
    /// The property <c>longitude</c>. The longitude coordinate of the position.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when unclear.
    /// </remarks>
    [JsonPropertyName("longitude")]
    public Coordinate? Longitude { get; init; }
    /// <summary>
    /// The property <c>height</c>. The height of the position.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when unclear or undefined.
    /// </remarks>
    [JsonPropertyName("height")]
    public Height? Height { get; init; }
    /// <summary>
    /// The property <c>geodeticSystem</c>. The geodetic system of the coordinate.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> depending on the situation.
    /// </remarks>
    [JsonPropertyName("geodeticSystem")]
    public Geodetic? GeodeticSystem { get; init; }
    /// <summary>
    /// The property <c>condition</c>. Extra information provided for the coordinate.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when unnecessary.
    /// <c>不明</c> when coordinate is unclear.
    /// </remarks>
    [JsonPropertyName("condition")]
    public string? Condition { get; init; }
}
