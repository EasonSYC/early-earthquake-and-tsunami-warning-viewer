using EasonEetwViewer.KyoshinMonitor.Abstractions;
using EasonEetwViewer.KyoshinMonitor.Dtos;

namespace EasonEetwViewer.Services;
internal interface IKmoniDto
{
    internal SensorType SensorChoice { get; }
    internal MeasurementType DataChoice { get; }
}
