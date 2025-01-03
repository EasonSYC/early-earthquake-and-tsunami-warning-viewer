namespace EasonEetwViewer.KyoshinMonitor.Dto.Enum;
/// <summary>
/// Represents the data type for the data map in kmoni.
/// </summary>
public enum KmoniDataType
{
    /// <summary>
    /// Real-time measured intensity. The value <c>jma</c>.
    /// </summary>
    MeasuredIntensity = 0,
    /// <summary>
    /// Peak (maximal) ground acceleration. The value <c>acmap</c>.
    /// </summary>
    PeakGroundAcceleration = 1,
    /// <summary>
    /// Peak (maximal) ground velocity. The value <c>vcmap</c>.
    /// </summary>
    PeakGroundVelocity = 2,
    /// <summary>
    /// Peak (maximal) ground displacement. The value <c>dcmap</c>.
    /// </summary>
    PeakGroundDisplacement = 3,
    /// <summary>
    /// Response spectrum for 0.125Hz PGV. The value <c>rsp0125</c>.
    /// </summary>
    Response0125 = 4,
    /// <summary>
    /// Response spectrum for 0.250Hz PGV. The value <c>rsp0250</c>.
    /// </summary>
    Response0250 = 5,
    /// <summary>
    /// Response spectrum for 0.500Hz PGV. The value <c>rsp0500</c>.
    /// </summary>
    Response0500 = 6,
    /// <summary>
    /// Response spectrum for 1.000Hz PGV. The value <c>rsp1000</c>.
    /// </summary>
    Response1000 = 7,
    /// <summary>
    /// Response spectrum for 2.000Hz PGV. The value <c>rsp2000</c>.
    /// </summary>
    Response2000 = 8,
    /// <summary>
    /// Response spectrum for 4.000Hz PGV. The value <c>rsp4000</c>.
    /// </summary>
    Response4000 = 9
}
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