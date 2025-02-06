using EasonEetwViewer.KyoshinMonitor.Dtos;
using EasonEetwViewer.KyoshinMonitor.Services;

namespace EasonEetwViewer.Services;
internal interface IKmoniDto
{
    internal SensorType SensorChoice { get; }
    internal MeasurementType DataChoice { get; }
}
