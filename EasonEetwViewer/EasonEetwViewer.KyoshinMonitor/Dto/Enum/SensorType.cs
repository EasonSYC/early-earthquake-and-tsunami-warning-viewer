namespace EasonEetwViewer.KyoshinMonitor.Dto.Enum;
/// <summary>
/// Represents the sensor type for the data map in kmoni.
/// </summary>
public enum SensorType
{
    /// <summary>
    /// Surface sensors, both K-NET and KiK-net. The value <c>s</c>.
    /// </summary>
    Surface = 0,
    /// <summary>
    /// Borehole sensors, onli KiK-net sensors. The value <c>b</c>.
    /// </summary>
    Borehole = 1
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
    /// <returns>A string that is used in the URI of kmoni.</returns>
    public static string ToUriString(this SensorType sensorType) => sensorType switch
    {
        SensorType.Surface => "s",
        SensorType.Borehole => "b",
        _ => "unknown",
    };

    /// <summary>
    /// Represents the enum in a string that is human-friendly.
    /// </summary>
    /// <param name="sensorType">The current instance of <c>SensorType</c></param>
    /// <returns>A string that is human-friendly.</returns>
    public static string ToReadableString(this SensorType sensorType) => sensorType switch
    {
        SensorType.Surface => "Surface",
        SensorType.Borehole => "Borehole",
        _ => "Unknown",
    };
}