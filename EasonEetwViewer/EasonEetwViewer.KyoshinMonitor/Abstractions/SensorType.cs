namespace EasonEetwViewer.KyoshinMonitor.Abstractions;
/// <summary>
/// Represents the sensor type for the data map in kmoni.
/// </summary>
public enum SensorType
{
    /// <summary>
    /// Surface sensors, both K-NET and KiK-net. The value <c>s</c>.
    /// </summary>
    Surface ,
    /// <summary>
    /// Borehole sensors, onli KiK-net sensors. The value <c>b</c>.
    /// </summary>
    Borehole
}