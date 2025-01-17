using EasonEetwViewer.KyoshinMonitor.Dto.Enum;

namespace EasonEetwViewer.Services;
internal interface IKmoniDto
{
    internal SensorType SensorChoice { get; }
    internal KmoniDataType DataChoice { get; }
}
