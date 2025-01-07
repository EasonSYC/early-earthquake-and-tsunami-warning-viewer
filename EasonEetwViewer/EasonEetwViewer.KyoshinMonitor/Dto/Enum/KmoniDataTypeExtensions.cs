using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasonEetwViewer.KyoshinMonitor.Dto.Enum;
/// <summary>
/// Provides extension methods for <c>KmoniDataType</c>.
/// </summary>
public static class KmoniDataTypeExtensions
{
    /// <summary>
    /// Represents the enum in a string that is used in the URI of the kmoni.
    /// </summary>
    /// <param name="kmoniDataType">The current instance of <c>KmoniDataType</c></param>
    /// <returns>A string that is used in the URI of kmoni.</returns>
    public static string ToUriString(this KmoniDataType kmoniDataType) => kmoniDataType switch
    {
        KmoniDataType.MeasuredIntensity => "jma",
        KmoniDataType.PeakGroundAcceleration => "acmap",
        KmoniDataType.PeakGroundVelocity => "vcmap",
        KmoniDataType.PeakGroundDisplacement => "dcmap",
        KmoniDataType.Response0125 => "rsp0125",
        KmoniDataType.Response0250 => "rsp0250",
        KmoniDataType.Response0500 => "rsp0500",
        KmoniDataType.Response1000 => "rsp1000",
        KmoniDataType.Response2000 => "rsp2000",
        KmoniDataType.Response4000 => "rsp4000",
        _ => "unknown",
    };

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