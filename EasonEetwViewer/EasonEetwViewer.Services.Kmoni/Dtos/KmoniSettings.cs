using System.Text.Json.Serialization;
using EasonEetwViewer.KyoshinMonitor.Abstractions;

namespace EasonEetwViewer.Services.Kmoni.Dtos;
/// <summary>
/// Represents the settings for the kmoni.
/// </summary>
public partial class KmoniSettings
{
    /// <summary>
    /// The sensor choice.
    /// </summary>
    [JsonPropertyName("SensorChoice")]
    public required SensorType SensorChoice { get; set; }
    /// <summary>
    /// The measurement choice.
    /// </summary>
    [JsonPropertyName("MeasurementChoice")]
    public required MeasurementType MeasurementChoice { get; set; }
}
