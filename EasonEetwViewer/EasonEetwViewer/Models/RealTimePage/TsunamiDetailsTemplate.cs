using EasonEetwViewer.Dmdata.Telegram.Dtos.Schema;
using EasonEetwViewer.Extensions;

namespace EasonEetwViewer.Models.RealTimePage;
/// <summary>
/// Represents a template for the current tsunami details.
/// </summary>
internal record TsunamiDetailsTemplate
{
    /// <summary>
    /// The informational text of the tsunami.
    /// </summary>
    public string? InformationalText { get; private init; }
    /// <summary>
    /// The expiry time of the information.
    /// </summary>
    public DateTimeOffset ExpiryTime { get; private init; }
    /// <summary>
    /// The update time of the information.
    /// </summary>
    public DateTimeOffset UpdateTime { get; private init; }
    /// <summary>
    /// The maximum warning type of the tsunami.
    /// </summary>
    public TsunamiWarningType MaxWarningType { get; private init; }
    /// <summary>
    /// Initializes a new instance of the <see cref="TsunamiDetailsTemplate"/> class.
    /// </summary>
    /// <param name="tsunami">The tsunami information telegram.</param>
    /// <param name="validDateTime">The time the information is valid until.</param>
    public TsunamiDetailsTemplate(TsunamiInformationSchema tsunami, DateTimeOffset validDateTime)
    {
        InformationalText = tsunami.ToInformationString();
        ExpiryTime = validDateTime;
        UpdateTime = tsunami.PressDateTime;
        MaxWarningType = tsunami.Body.Tsunami?.Forecasts?
            .Max(forecast => forecast.Kind.Code.ToTsunamiWarningType())
            ?? TsunamiWarningType.None;
    }
}
