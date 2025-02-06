using EasonEetwViewer.KyoshinMonitor.Dtos;
using EasonEetwViewer.KyoshinMonitor.Services;

namespace EasonEetwViewer.Services;
internal class KmoniDefaultOptions : IKmoniDto
{
    SensorType IKmoniDto.SensorChoice => SensorType.Surface;
    MeasurementType IKmoniDto.DataChoice => MeasurementType.MeasuredIntensity;
}
