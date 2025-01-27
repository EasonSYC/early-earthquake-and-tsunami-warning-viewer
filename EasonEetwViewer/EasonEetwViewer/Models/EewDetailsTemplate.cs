using CommunityToolkit.Mvvm.ComponentModel;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.EewInformation;

namespace EasonEetwViewer.Models;
internal class EewDetailsTemplate : ObservableObject
{
    internal DateTimeOffset ExpiryTime { get; private init; }
    internal DateTimeOffset UpdateTime { get; private init; }
    internal string? EventId { get; private init; }
    internal bool IsCancelled { get; private init; }
    internal bool IsLastInfo { get; private init; }
    internal bool? IsWarning { get; private init; }
    internal Earthquake? Earthquake { get; private init; }
    internal string? InformationalText { get; private init; }
    internal CancellationTokenSource TokenSource { get; private init; }
    internal CancellationToken Token { get; private init; }
    internal EewDetailsTemplate(DateTimeOffset expiryTime, DateTimeOffset updateTime, string? eventId, bool isCancelled, bool isLastInfo, bool? isWarning, Earthquake? earthquake, string? informationalText)
    {
        ExpiryTime = expiryTime;
        UpdateTime = updateTime;
        EventId = eventId;
        IsCancelled = isCancelled;
        IsLastInfo = isLastInfo;
        IsWarning = isWarning;
        Earthquake = earthquake;
        InformationalText = informationalText;
        TokenSource = new();
        Token = TokenSource.Token;
    }
}
