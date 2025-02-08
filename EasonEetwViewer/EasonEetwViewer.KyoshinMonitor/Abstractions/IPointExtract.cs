using EasonEetwViewer.KyoshinMonitor.Dtos;
using SkiaSharp;

namespace EasonEetwViewer.KyoshinMonitor.Abstractions;

/// <summary>
/// Represents the functionality to extract the colour of points from a <see cref="SKBitmap"/>.
/// </summary>
public interface IPointExtract
{
    /// <summary>
    /// Extract the colours from the Bitmap.
    /// </summary>
    /// <param name="bitmap">The <c>SKBitmap</c> to have its pixels extracted from the <see cref="SKBitmap"/>.</param>
    /// <param name="kikNetOnly">Whether <c>KiK-net</c> sensors only should be extracted.</param>
    /// <returns>A collection of pairs of observation points and their colours in <see cref="SKColor"/>.</returns>
    public IEnumerable<(ObservationPoint point, SKColor colour)> ExtractColours(SKBitmap bitmap, bool kikNetOnly = false);
}
