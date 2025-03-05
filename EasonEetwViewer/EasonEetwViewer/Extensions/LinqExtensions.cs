namespace EasonEetwViewer.Extensions;

/// <summary>
/// Provides extension methods for LINQ.
/// </summary>
internal static class LinqExtensions
{
    /// <summary>
    /// Filters a sequence of values for values that are not <see langword="null"/>.
    /// </summary>
    /// <typeparam name="T">The type in the enumerable to be filtered.</typeparam>
    /// <param name="enumerable">The enumerable that is to be filtered.</param>
    /// <returns>An enumerable that contains the elements that are not null.</returns>
    public static IEnumerable<T> NotNull<T>(this IEnumerable<T?> enumerable) where T : class
        => enumerable
            .Where(e => e is not null)
            .Select(e => e!);
}
