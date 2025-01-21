using EasonEetwViewer.KyoshinMonitor.Dto.Enum;

namespace EasonEetwViewer.Services;
internal interface IKmoniDto
{
    internal SensorType SensorChoice { get; }
    internal MeasurementType DataChoice { get; }
}
