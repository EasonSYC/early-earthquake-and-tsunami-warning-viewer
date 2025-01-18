using EasonEetwViewer.KyoshinMonitor.Dto.Enum;

namespace EasonEetwViewer.Services.KmoniOption;
internal interface IKmoniDto
{
    internal SensorType SensorChoice { get; }
    internal KmoniDataType DataChoice { get; }
}
