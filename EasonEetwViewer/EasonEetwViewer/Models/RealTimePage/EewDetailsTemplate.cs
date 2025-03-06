using EasonEetwViewer.Dmdata.Telegram.Dtos.EewInformation;
using EasonEetwViewer.Dmdata.Telegram.Dtos.EewInformation.Enum.Range;
using EasonEetwViewer.Dmdata.Telegram.Dtos.Schema;
using EasonEetwViewer.Extensions;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Models.RealTimePage;
/// <summary>
/// Represents the template for the details of an EEW.
/// </summary>
internal record EewDetailsTemplate
{
    /// <summary>
    /// The expiry time of the EEW.
    /// </summary>
    public DateTimeOffset ExpiryTime { get; private init; }
    /// <summary>
    /// The last updated time of the EEW Information.
    /// </summary>
    public DateTimeOffset UpdateTime { get; private init; }
    /// <summary>
    /// The serial number of the EEW.
    /// </summary>
    public int Serial { get; private init; }
    /// <summary>
    /// The event ID of the EEW.
    /// </summary>
    public string? EventId { get; private init; }
    /// <summary>
    /// The warning type of the EEW.
    /// </summary>
    public EewWarningType EewWarningType { get; private init; }
    /// <summary>
    /// The earthquake reported by the EEW.
    /// </summary>
    public Earthquake? Earthquake { get; private init; }
    /// <summary>
    /// The intensity reported by the EEW.
    /// </summary>
    public IntensityInfo? IntensityInfo { get; private init; }
    /// <summary>
    /// The informational text of the EEW.
    /// </summary>
    public string InformationalText { get; private init; }
    /// <summary>
    /// Whether the hypocentre is assumed.
    /// </summary>
    public bool IsAssumedHypocentre { get; private init; }
    /// <summary>
    /// Whether the information is based on only one observation point.
    /// </summary>
    public bool IsOnePointInfo { get; private init; }
    /// <summary>
    /// Whether the maximum intensity is exact, or is a lower bound.
    /// </summary>
    public bool IsAbove { get; private init; }
    /// <summary>
    /// The text before the intensity text.
    /// </summary>
    public string BeforeIntensityText
        => IsAbove
            ? RealtimePageResources.IntensityOverTextBefore
            : string.Empty;
    /// <summary>
    /// The text after the intensity text.
    /// </summary>
    public string AfterIntensityText
        => IsAbove
            ? RealtimePageResources.IntensityOverTextAfter
            : string.Empty;
    /// <summary>
    /// The maximum intensity of the EEW, to be displayed.
    /// </summary>
    public Enum? MaxIntensityEnum
        => IsAbove
            ? IntensityInfo?.ForecastMaxInt.From
            : IntensityInfo?.ForecastMaxInt.To;
    /// <summary>
    /// The cancellation token source related with this EEW issue.
    /// </summary>
    public CancellationTokenSource TokenSource { get; private init; }
    /// <summary>
    /// The cancellation token related with this EEW issue.
    /// </summary>
    public CancellationToken Token { get; private init; }
    /// <summary>
    /// Initializes a new instance of the <see cref="EewDetailsTemplate"/> class.
    /// </summary>
    /// <param name="eew">The EEW Telegram.</param>
    /// <param name="expiryTime">The expiry time of this information.</param>
    /// <param name="serial">The serial of this information.</param>
    public EewDetailsTemplate(EewInformationSchema eew, DateTimeOffset expiryTime, int serial)
    {
        ExpiryTime = expiryTime;
        UpdateTime = eew.PressDateTime;
        Serial = serial;
        EventId = eew.EventId;
        EewWarningType = eew.ToWarningType();
        Earthquake = eew.Body.Earthquake;
        IntensityInfo = eew.Body.Intensity;
        InformationalText = eew.ToInformationString();
        IsAssumedHypocentre =
            eew.Body.Earthquake is not null &&
            eew.Body.Earthquake.IsAssumedHypocentre();
        IsOnePointInfo =
            eew.Body.Earthquake is not null &&
            eew.Body.Earthquake.Hypocentre.Accuracy.IsOnePointInfo();
        IsAbove =
            eew.Body.Intensity is not null &&
            eew.Body.Intensity.ForecastMaxInt.To is IntensityUpper.Above;
        TokenSource = new();
        Token = TokenSource.Token;
    }
}
