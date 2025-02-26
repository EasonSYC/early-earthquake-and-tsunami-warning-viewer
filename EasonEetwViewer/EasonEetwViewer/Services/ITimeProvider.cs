namespace EasonEetwViewer.Services;
internal interface ITimeProvider
{
    DateTimeOffset DateTimeOffsetNow();
    DateTimeOffset DateTimeOffsetUtcNow();
    DateTime DateTimeNow();
    DateTime DateTimeUtcNow();
}
