using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasonEetwViewer.KyoshinMonitor.Dto.Enum;

namespace EasonEetwViewer.Services.KmoniOptions;
internal class KmoniDefaultOptions : IKmoniDto
{
    SensorType IKmoniDto.SensorChoice => SensorType.Surface;
    KmoniDataType IKmoniDto.DataChoice => KmoniDataType.MeasuredIntensity;
}
