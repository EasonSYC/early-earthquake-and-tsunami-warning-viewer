using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasonEetwViewer.KyoshinMonitor.Dto;
using SkiaSharp;

namespace EasonEetwViewer.KyoshinMonitor;
public interface IPointExtract
{
    public List<(ObservationPoint point, SKColor colour)> ExtractColours(SKBitmap bitmap, bool kikNetOnly = false);
}
