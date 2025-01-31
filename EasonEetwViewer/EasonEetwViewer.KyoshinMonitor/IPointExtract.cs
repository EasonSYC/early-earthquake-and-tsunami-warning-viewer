using EasonEetwViewer.KyoshinMonitor.Dto;
using SkiaSharp;

namespace EasonEetwViewer.KyoshinMonitor;
public interface IPointExtract
{
    public List<(ObservationPoint point, SKColor colour)> ExtractColours(SKBitmap bitmap, bool kikNetOnly = false);
}
