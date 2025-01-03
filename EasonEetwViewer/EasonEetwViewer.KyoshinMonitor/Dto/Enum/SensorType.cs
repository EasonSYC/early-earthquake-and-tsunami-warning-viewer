namespace EasonEetwViewer.KyoshinMonitor.Dto.Enum;
/// <summary>
/// Represents the sensor type for the data map in kmoni.
/// </summary>
public enum SensorType
{
    /// <summary>
    /// Unknown. Default value.
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// Surface sensors, both K-NET and KiK-net. The value <c>s</c>.
    /// </summary>
    Surface = 1,
    /// <summary>
    /// Borehole sensors, onli KiK-net sensors. The value <c>b</c>.
    /// </summary>
    Borehole = 2
}
/// <summary>
/// Provides extension methods for <c>SensorType</c>.
/// </summary>
public static class SensorTypeExtensions
{
    /// <summary>
    /// Represents the enum in a string that is used in the URI of the kmoni.
    /// </summary>
    /// <param name="sensorType">The current instance of <c>SensorType</c></param>
    /// <returns></returns>
    public static string ToUriString(this SensorType sensorType) => sensorType switch
    {
        SensorType.Surface => "s",
        SensorType.Borehole => "b",
        SensorType.Unknown or _ => "unknown",
    };
}