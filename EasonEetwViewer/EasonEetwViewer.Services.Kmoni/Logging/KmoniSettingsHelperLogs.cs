using EasonEetwViewer.KyoshinMonitor.Abstractions;
using EasonEetwViewer.KyoshinMonitor.Services;
using EasonEetwViewer.Services.Kmoni;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.KyoshinMonitor;
/// <summary>
/// Represents the log messages used in <see cref="KmoniSettingsHelper"/>.
/// </summary>
internal static partial class KmoniSettingsHelperLogs
{
    /// <summary>
    /// Log when instantiated.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    [LoggerMessage(
        EventId = 0,
        EventName = nameof(Instantiated),
        Level = LogLevel.Information,
        Message = "Instantiated.")]
    public static partial void Instantiated(
        this ILogger<KmoniSettingsHelper> logger);

    /// <summary>
    /// Log when an I/O operation failed..
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="filePath">The path to the file.</param>
    [LoggerMessage(
        EventId = 1,
        EventName = nameof(IOFailed),
        Level = LogLevel.Error,
        Message = "File Read/Write Failed: `{FilePath}`.")]
    public static partial void IOFailed(
        this ILogger<KmoniSettingsHelper> logger, string filePath);

    /// <summary>
    /// Log when sensor choice changed.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="sensorChoice">The new sensor choice.</param>
    [LoggerMessage(
        EventId = 2,
        EventName = nameof(SensorChoiceChanged),
        Level = LogLevel.Information,
        Message = "Sensor choice changed to: `{SensorChoice}`.")]
    public static partial void SensorChoiceChanged(
        this ILogger<KmoniSettingsHelper> logger, SensorType sensorChoice);

    /// <summary>
    /// Log when measurement choice changed.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="measurementChoice">The new measurement choice.</param>
    [LoggerMessage(
        EventId = 3,
        EventName = nameof(MeasurementChoiceChanged),
        Level = LogLevel.Information,
        Message = "Measurement choice changed to: `{MeasurementChoice}`.")]
    public static partial void MeasurementChoiceChanged(
        this ILogger<KmoniSettingsHelper> logger, MeasurementType measurementChoice);
}
