using System.Text.Json.Serialization;
using EasonEetwViewer.KyoshinMonitor.Abstractions;

namespace EasonEetwViewer.Services.KmoniOptions;
/// <summary>
/// An implementation of <see cref="IKmoniDto"/> which provides serialisable options.
/// </summary>
/// <param name="sensorChoice">The choice for the sensor.</param>
/// <param name="dataChoice">The choice for the data.</param>
internal class KmoniSerialisableOptions(SensorType sensorChoice, MeasurementType dataChoice) : IKmoniDto
{
    /// <inheritdoc/>
    [JsonPropertyName("sensorChoice")]
    public SensorType SensorChoice { get; private init; } = sensorChoice;
    /// <inheritdoc/>
    [JsonPropertyName("dataChoice")]
    public MeasurementType DataChoice { get; private init; } = dataChoice;
}
