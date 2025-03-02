using EasonEetwViewer.KyoshinMonitor.Abstractions;

namespace EasonEetwViewer.Services.KmoniOptions;
/// <summary>
/// Defines choices for the Kmoni monitor.
/// </summary>
internal interface IKmoniDto
{
    /// <summary>
    /// The choice for the sensor to display.
    /// </summary>
    SensorType SensorChoice { get; }
    /// <summary>
    /// The choice for the data to display.
    /// </summary>
    MeasurementType DataChoice { get; }
}
