using EasonEetwViewer.KyoshinMonitor.Dto.Enum;

namespace EasonEetwViewer.KyoshinMonitor;
public interface IImageFetch
{
    public Task<byte[]> GetByteArrayAsync(MeasurementType measurementType, SensorType sensorType, DateTimeOffset dateTime);
}
