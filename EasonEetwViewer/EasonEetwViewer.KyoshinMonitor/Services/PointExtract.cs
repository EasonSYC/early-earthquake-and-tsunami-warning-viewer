using System.Text.Json;
using EasonEetwViewer.KyoshinMonitor.Dtos;
using EasonEetwViewer.KyoshinMonitor.Interfaces;
using SkiaSharp;

namespace EasonEetwViewer.KyoshinMonitor.Services;
/// <summary>
/// Default implementation of <see cref="IPointExtract"/>
/// </summary>
public sealed class PointExtract : IPointExtract
{
    /// <summary>
    /// Creates an instance of <see cref="PointExtract"/> by specifying the collection of observation points.
    /// </summary>
    /// <param name="points">The collection of observation points.</param>
    private PointExtract(IEnumerable<ObservationPoint> points) => _points = points;
    /// <summary>
    /// Creates an instance of <see cref="PointExtract"/> by reading from a JSON text file.
    /// </summary>
    /// <param name="filePath">The path to the file that stores the collection of observation points.</param>
    /// <returns>The new instance of <see cref="PointExtract"/></returns>
    public static PointExtract FromFile(string filePath)
        => new(JsonSerializer.Deserialize<IEnumerable<ObservationPoint>>(File.ReadAllText(filePath)) ?? []);
    /// <summary>
    /// The collection of observation points.
    /// </summary>
    private readonly IEnumerable<ObservationPoint> _points;
    /// <inheritdoc/>
    public IEnumerable<(ObservationPoint point, SKColor colour)> ExtractColours(SKBitmap bitmap, bool kikNetOnly = false) => _points
        .Where(p => !p.IsSuspended && p.Point is not null)
        .Where(p => !(kikNetOnly && p.Type != PointType.KiK))
        .Select(p => (p, colour: bitmap.GetPixel(p.Point.X, p.Point.Y)))
        .ToArray() // deferred execution throws System.ExecutionEngineException
        .Where(pc => pc.colour.Alpha != 0);
}