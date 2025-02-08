using EasonEetwViewer.KyoshinMonitor.Abstractions;

namespace EasonEetwViewer.Services;
internal interface IKmoniDto
{
    internal SensorType SensorChoice { get; }
    internal MeasurementType DataChoice { get; }
}
