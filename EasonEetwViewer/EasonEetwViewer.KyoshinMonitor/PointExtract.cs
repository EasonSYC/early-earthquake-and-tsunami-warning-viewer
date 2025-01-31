using System.Text.Json;
using EasonEetwViewer.KyoshinMonitor.Dto;
using EasonEetwViewer.KyoshinMonitor.Dto.Enum;
using SkiaSharp;

namespace EasonEetwViewer.KyoshinMonitor;
/// <summary>
/// Represents the functionality to extract the colour of points from a <c>SKBitmap</c>.
/// </summary>
/// <param name="filePath">The path to the file that stores the list of observation points.</param>
public class PointExtract(string filePath) : IPointExtract
{
    /// <summary>
    /// The list of observation points.
    /// </summary>
    private readonly List<ObservationPoint> _points = JsonSerializer.Deserialize<List<ObservationPoint>>(File.ReadAllText(filePath)) ?? [];
    /// <summary>
    /// Writes the list of observation points to the specified file path.
    /// </summary>
    /// <param name="filePath">The path to write the file to.</param>
    public void WritePoints(string filePath) => File.WriteAllText(filePath, JsonSerializer.Serialize(_points));
    /// <summary>
    /// Extract the colours from the bitmap using the list of observation points.
    /// </summary>
    /// <param name="bitmap">The <c>SKBitmap</c> to have its pixels extracted from.</param>
    /// <param name="kikNetOnly">Whether <c>KiK-net</c> sensors only should be extracted.</param>
    /// <returns>A list of pairs of observation points and their colours.</returns>
    public List<(ObservationPoint point, SKColor colour)> ExtractColours(SKBitmap bitmap, bool kikNetOnly = false)
    {
        List<(ObservationPoint point, SKColor colour)> result = [];
        foreach (ObservationPoint point in _points)
        {
            if (!point.IsSuspended && point.Point is not null && (!kikNetOnly || point.Type == PointType.KiK))
            {
                SKColor color = bitmap.GetPixel(point.Point.X, point.Point.Y);
                if (color.Alpha != 0)
                {
                    result.Add((point, color));
                }
            }
        }

        return result;
    }
}
