namespace EasonEetwViewer.KyoshinMonitor.Dto.Enum;
public enum SensorType
{
    Unknown = 0,
    Surface = 1,
    Borehole = 2
}

public static class SensorTypeExtensions
{
    public static string ToUriString(this SensorType sensorType) => sensorType switch
    {
        SensorType.Surface => "s",
        SensorType.Borehole => "b",
        _ => "unknown",
    };
}