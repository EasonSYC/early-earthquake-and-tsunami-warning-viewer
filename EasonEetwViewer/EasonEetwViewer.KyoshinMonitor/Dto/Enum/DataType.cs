namespace EasonEetwViewer.KyoshinMonitor.Dto.Enum;
/// <summary>
/// Represents the data type for the data map in kmoni.
/// </summary>
public enum KmoniDataType
{
    /// <summary>
    /// Unknown. Default value.
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// Real-time measured intensity. The value <c>jma</c>.
    /// </summary>
    MeasuredIntensity = 1,
    /// <summary>
    /// Peak (maximal) ground acceleration. The value <c>acmap</c>.
    /// </summary>
    PeakGroundAcceleration = 2,
    /// <summary>
    /// Peak (maximal) ground velocity. The value <c>vcmap</c>.
    /// </summary>
    PeakGroundVelocity = 3,
    /// <summary>
    /// Peak (maximal) ground displacement. The value <c>dcmap</c>.
    /// </summary>
    PeakGroundDisplacement = 4,
    /// <summary>
    /// Response spectrum for 0.125Hz PGV. The value <c>rsp0125</c>.
    /// </summary>
    Response0125 = 5,
    /// <summary>
    /// Response spectrum for 0.250Hz PGV. The value <c>rsp0250</c>.
    /// </summary>
    Response0250 = 6,
    /// <summary>
    /// Response spectrum for 0.500Hz PGV. The value <c>rsp0500</c>.
    /// </summary>
    Response0500 = 7,
    /// <summary>
    /// Response spectrum for 1.000Hz PGV. The value <c>rsp1000</c>.
    /// </summary>
    Response1000 = 8,
    /// <summary>
    /// Response spectrum for 2.000Hz PGV. The value <c>rsp2000</c>.
    /// </summary>
    Response2000 = 9,
    /// <summary>
    /// Response spectrum for 4.000Hz PGV. The value <c>rsp4000</c>.
    /// </summary>
    Response4000 = 10
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
    /// <returns></returns>
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
}