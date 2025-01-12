using EasonEetwViewer.KyoshinMonitor.Dto.Enum;

namespace EasonEetwViewer.Models;
/// <summary>
/// Provides extension methods for <c>KmoniDataType</c>.
/// </summary>
public static class KmoniDataTypeExtensions
{
    /// <summary>
    /// Represents the enum in a string that is human-friendly.
    /// </summary>
    /// <param name="kmoniDataType">The current instance of <c>KmoniDataType</c></param>
    /// <returns>A string that is human-friendly.</returns>
    public static string ToReadableString(this KmoniDataType kmoniDataType) => kmoniDataType switch
    {
        KmoniDataType.MeasuredIntensity => "Measured Intensity",
        KmoniDataType.PeakGroundAcceleration => "Peak Ground Acceleration (PGA)",
        KmoniDataType.PeakGroundVelocity => "Peak Ground Velocity (PGV)",
        KmoniDataType.PeakGroundDisplacement => "Peak Ground Displacement (PGD)",
        KmoniDataType.Response0125 => "0.125 Hz PGV Response Spectrum",
        KmoniDataType.Response0250 => "0.250 Hz PGV Response Spectrum",
        KmoniDataType.Response0500 => "0.500 Hz PGV Response Spectrum",
        KmoniDataType.Response1000 => "1.000 Hz PGV Response Spectrum",
        KmoniDataType.Response2000 => "2.000 Hz PGV Response Spectrum",
        KmoniDataType.Response4000 => "4.000 Hz PGV Response Spectrum",
        _ => "Unknown",
    };
}