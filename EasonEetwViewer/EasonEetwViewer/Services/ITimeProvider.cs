namespace EasonEetwViewer.Services;
internal interface ITimeProvider
{
    public DateTimeOffset DateTimeOffsetNow();
    public DateTimeOffset DateTimeOffsetUtcNow();
    public DateTime DateTimeNow();
    public DateTime DateTimeUtcNow();
}
