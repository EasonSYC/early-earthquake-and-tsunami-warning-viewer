using EasonEetwViewer.KyoshinMonitor.Abstractions;
using EasonEetwViewer.KyoshinMonitor.Dtos;
using SkiaSharp;

namespace EasonEetwViewer.KyoshinMonitor.Services;
/// <summary>
/// Default implementation of <see cref="IPointExtract"/>
/// </summary>
internal sealed class PointExtract : IPointExtract
{
    /// <summary>
    /// Creates an instance of <see cref="PointExtract"/> by specifying the collection of observation points.
    /// </summary>
    /// <param name="points">The collection of observation points.</param>
    internal PointExtract(IEnumerable<ObservationPoint> points) => _points = points;
    /// <summary>
    /// The collection of observation points.
    /// </summary>
    private readonly IEnumerable<ObservationPoint> _points;
    /// <inheritdoc/>
    public IEnumerable<(ObservationPoint point, SKColor colour)> ExtractColours(SKBitmap bitmap, bool kikNetOnly = false)
        => _points
            .Where(p => !p.IsSuspended && p.Point is not null)
            .Where(p => !(kikNetOnly && p.Type != PointType.KiK))
            .Select(p => (p, colour: bitmap.GetPixel(p.Point.X, p.Point.Y)))
            .ToArray() // deferred execution throws System.ExecutionEngineException
            .Where(pc => pc.colour.Alpha != 0);
}