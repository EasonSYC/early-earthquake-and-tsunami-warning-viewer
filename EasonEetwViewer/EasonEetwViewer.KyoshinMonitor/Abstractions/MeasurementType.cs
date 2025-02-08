namespace EasonEetwViewer.KyoshinMonitor.Abstractions;
/// <summary>
/// Represents the data type for the data map in kmoni.
/// </summary>
public enum MeasurementType
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
