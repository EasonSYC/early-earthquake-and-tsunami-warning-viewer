using EasonEetwViewer.KyoshinMonitor.Abstractions;

namespace EasonEetwViewer.Services;
internal class KmoniDefaultOptions : IKmoniDto
{
    SensorType IKmoniDto.SensorChoice => SensorType.Surface;
    MeasurementType IKmoniDto.DataChoice => MeasurementType.MeasuredIntensity;
}
