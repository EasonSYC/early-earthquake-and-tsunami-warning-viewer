using EasonEetwViewer.KyoshinMonitor.Dto;
using SkiaSharp;

namespace EasonEetwViewer.KyoshinMonitor;
public interface IPointExtract
{
    public IEnumerable<(ObservationPoint point, SKColor colour)> ExtractColours(SKBitmap bitmap, bool kikNetOnly = false);
}
