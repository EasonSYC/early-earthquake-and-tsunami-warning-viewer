using System.Text.Json;
using EasonEetwViewer.KyoshinMonitor.Dto;
using SkiaSharp;

namespace EasonEetwViewer.KyoshinMonitor;
public class KmoniPointExtract(string filePath)
{
    List<ObservationPoint> Points = JsonSerializer.Deserialize<List<ObservationPoint>>(File.ReadAllText(filePath)) ?? [];

    public void WritePoints(string filePath) => File.WriteAllText(filePath, JsonSerializer.Serialize(Points));

    public List<(ObservationPoint point, SKColor colour)> ExtractColours(SKBitmap bitmap)
    {
        List<(ObservationPoint point, SKColor colour)> result = [];
        foreach (ObservationPoint point in Points)
        {
            if (!point.IsSuspended && point.Point is not null)
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
