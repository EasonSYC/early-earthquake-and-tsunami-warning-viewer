namespace EasonEetwViewer.Services.TimeProvider;
/// <summary>
/// Provides time-related information.
/// </summary>
internal interface ITimeProvider
{
    /// <summary>
    /// Returns the current time.
    /// </summary>
    /// <returns>The current time in <see cref="DateTimeOffset"/>.</returns>
    /// <remarks>
    /// It is NOT guarenteed that the offset will be in the current timezone.
    /// </remarks>
    DateTimeOffset Now();
    /// <summary>
    /// Converts the given <see cref="DateTimeOffset"/> to JST.
    /// </summary>
    /// <param name="dateTimeOffset">The <see cref="DateTimeOffset"/> to be converted.</param>
    /// <returns>The new <see cref="DateTimeOffset"/> whose time offset is in JST.</returns>
    DateTimeOffset ConvertToJst(DateTimeOffset dateTimeOffset);
}
