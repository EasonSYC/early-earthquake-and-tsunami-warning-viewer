namespace EasonEetwViewer.Models.RealTimePage;

/// <summary>
/// Represents the EEW type that is to be displayed.
/// </summary>
internal enum EewWarningType
{
    /// <summary>
    /// An EEW Forecast (that is not cancelled or the final info).
    /// </summary>
    Forecast,
    /// <summary>
    /// An EEW Warning (that is not cancelled or the final info).
    /// </summary>
    Warning,
    /// <summary>
    /// An EEW that is the final information (that is not cancelled).
    /// </summary>
    Final,
    /// <summary>
    /// An EEW that has been cancelled.
    /// </summary>
    Cancelled
}
