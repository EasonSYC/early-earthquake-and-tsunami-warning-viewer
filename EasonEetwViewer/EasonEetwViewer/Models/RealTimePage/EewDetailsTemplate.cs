using EasonEetwViewer.Telegram.Dtos.EewInformation;

namespace EasonEetwViewer.Models.RealTimePage;
internal record EewDetailsTemplate
{
    internal DateTimeOffset ExpiryTime { get; private init; }
    internal DateTimeOffset UpdateTime { get; private init; }
    internal int Serial { get; private init; }
    internal string? EventId { get; private init; }
    internal bool IsCancelled { get; private init; }
    internal bool IsLastInfo { get; private init; }
    internal bool IsWarning { get; private init; }
    internal Earthquake? Earthquake { get; private init; }
    internal IntensityInfo? IntensityInfo { get; private init; }
    internal string? InformationalText { get; private init; }
    internal CancellationTokenSource TokenSource { get; private init; }
    internal CancellationToken Token { get; private init; }
    internal EewDetailsTemplate(DateTimeOffset expiryTime, DateTimeOffset updateTime, int serial, string? eventId, bool isCancelled, bool isLastInfo, bool isWarning, Earthquake? earthquake, IntensityInfo? intensityInfo, string? informationalText)
    {
        ExpiryTime = expiryTime;
        UpdateTime = updateTime;
        Serial = serial;
        EventId = eventId;
        IsCancelled = isCancelled;
        IsLastInfo = isLastInfo;
        IsWarning = isWarning;
        Earthquake = earthquake;
        IntensityInfo = intensityInfo;
        InformationalText = informationalText;
        TokenSource = new();
        Token = TokenSource.Token;
    }
}
