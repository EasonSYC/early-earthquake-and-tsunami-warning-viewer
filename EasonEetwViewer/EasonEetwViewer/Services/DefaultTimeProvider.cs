namespace EasonEetwViewer.Services;
internal class DefaultTimeProvider : ITimeProvider
{
    public DateTimeOffset DateTimeOffsetNow() => DateTimeOffset.Now;
    public DateTimeOffset DateTimeOffsetUtcNow() => DateTimeOffset.UtcNow;
    public DateTime DateTimeNow() => DateTime.Now;
    public DateTime DateTimeUtcNow() => DateTime.UtcNow;
}
