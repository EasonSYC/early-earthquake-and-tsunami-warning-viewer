namespace EasonEetwViewer.Models.RealTimePage;
internal record TsunamiDetailsTemplate
{
    internal string? InformationalText { get; private init; }
    internal DateTimeOffset ExpiryTime { get; private init; }
    internal DateTimeOffset UpdateTime { get; private init; }
    internal TsunamiWarningType MaxWarningType { get; private init; }
    internal TsunamiDetailsTemplate(string informationalText, DateTimeOffset expiryType, DateTimeOffset updateTime, TsunamiWarningType maxWarningType)
    {
        InformationalText = informationalText;
        ExpiryTime = expiryType;
        UpdateTime = updateTime;
        MaxWarningType = maxWarningType;
    }
}
