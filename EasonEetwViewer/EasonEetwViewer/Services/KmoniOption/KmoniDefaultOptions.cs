using EasonEetwViewer.KyoshinMonitor.Dto.Enum;

namespace EasonEetwViewer.Services.KmoniOption;
internal class KmoniDefaultOptions : IKmoniDto
{
    SensorType IKmoniDto.SensorChoice => SensorType.Surface;
    KmoniDataType IKmoniDto.DataChoice => KmoniDataType.MeasuredIntensity;
}
