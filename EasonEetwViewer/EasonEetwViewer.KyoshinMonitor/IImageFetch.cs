using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasonEetwViewer.KyoshinMonitor.Dto.Enum;

namespace EasonEetwViewer.KyoshinMonitor;
public interface IImageFetch
{
    public Task<byte[]> GetByteArrayAsync(MeasurementType measurementType, SensorType sensorType, DateTimeOffset dateTime);
}
