using EasonEetwViewer.KyoshinMonitor.Dto.Enum;

namespace EasonEetwViewer.Models;
internal class ApplicationOptions
{
    internal Tuple<SensorType, string> SensorChoice { get; set; }
        = new(SensorType.Surface, SensorType.Surface.ToReadableString());
    internal Tuple<KmoniDataType, string> DataChoice { get; set; }
        = new(KmoniDataType.MeasuredIntensity, KmoniDataType.MeasuredIntensity.ToReadableString());
}
