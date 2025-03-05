using EasonEetwViewer.Dmdata.Telegram.Dtos.EewInformation;
using EasonEetwViewer.Dmdata.Telegram.Dtos.Schema;
using EasonEetwViewer.Extensions;

namespace EasonEetwViewer.Models.RealTimePage;
internal record EewDetailsTemplate
{
    public DateTimeOffset ExpiryTime { get; private init; }
    public DateTimeOffset UpdateTime { get; private init; }
    public int Serial { get; private init; }
    public string? EventId { get; private init; }
    public bool IsCancelled { get; private init; }
    public bool IsLastInfo { get; private init; }
    public bool IsWarning { get; private init; }
    public Earthquake? Earthquake { get; private init; }
    public IntensityInfo? IntensityInfo { get; private init; }
    public string? InformationalText { get; private init; }
    public CancellationTokenSource TokenSource { get; private init; }
    public CancellationToken Token { get; private init; }
    public EewDetailsTemplate(EewInformationSchema eew, DateTimeOffset expiryTime,  int serial)
    {
        ExpiryTime = expiryTime;
        UpdateTime = eew.PressDateTime;
        Serial = serial;
        EventId = eew.EventId;
        IsCancelled = eew.Body.IsCancelled;
        IsLastInfo = eew.Body.IsLastInfo;
        IsWarning = eew.Body.IsWarning ?? false;
        Earthquake = eew.Body.Earthquake;
        IntensityInfo = eew.Body.Intensity;
        InformationalText = eew.ToInformationString());
        TokenSource = new();
        Token = TokenSource.Token;
    }
}
