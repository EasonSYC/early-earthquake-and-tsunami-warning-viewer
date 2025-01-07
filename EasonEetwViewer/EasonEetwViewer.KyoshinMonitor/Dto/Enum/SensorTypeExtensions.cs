namespace EasonEetwViewer.KyoshinMonitor.Dto.Enum;
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