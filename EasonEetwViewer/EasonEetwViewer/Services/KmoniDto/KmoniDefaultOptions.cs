using EasonEetwViewer.KyoshinMonitor.Abstractions;

namespace EasonEetwViewer.Services.KmoniOptions;
/// <summary>
/// The default options for Kmoni.
/// </summary>
internal class KmoniDefaultOptions : IKmoniDto
{
    /// <inheritdoc/>
    public SensorType SensorChoice
        => SensorType.Surface;
    /// <inheritdoc/>
    public MeasurementType DataChoice
        => MeasurementType.MeasuredIntensity;
}
