namespace EasonEetwViewer.Services.TimeProvider;
/// <summary>
/// The default implementation of <see cref="ITimeProvider"/>.
/// </summary>
internal class DefaultTimeProvider : ITimeProvider
{
    /// <inheritdoc/>
    public DateTimeOffset Now()
        => DateTimeOffset.Now;
    /// <inheritdoc/>
    public DateTimeOffset ConvertToJst(DateTimeOffset dateTimeOffset)
        => dateTimeOffset.ToOffset(new TimeSpan(9, 0, 0));
}
