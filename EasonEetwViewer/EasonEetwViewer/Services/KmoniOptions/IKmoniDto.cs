using EasonEetwViewer.KyoshinMonitor.Dto.Enum;

namespace EasonEetwViewer.Services.KmoniOptions;
internal interface IKmoniDto
{
    internal SensorType SensorChoice { get; }
    internal KmoniDataType DataChoice { get; }
}
