using System.Collections.Specialized;

namespace EasonEetwViewer.Api.Extensions;
/// <summary>
/// Provides extension methods for <see cref="NameValueCollection"/>.
/// </summary>
internal static class NameValueCollectionExtensions
{
    /// <summary>
    /// Adds a parameter to the query if the value is not null.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="query">The query for it to be added to.</param>
    /// <param name="value">The value to be added.</param>
    /// <param name="key">The key for the value.</param>
    /// <returns>The query where it was added to, to chain the calls.</returns>
    public static NameValueCollection AddIfNotNull<T>(
        this NameValueCollection query,
        T? value,
        string key)
        => AddIfNotNull(query, value, key, x => x?.ToString());
    /// <summary>
    /// Adds a parameter to the query if the value is not null, using the given selector.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="query">The query for it to be added to.</param>
    /// <param name="value">The value to be added.</param>
    /// <param name="key">The key for the value.</param>
    /// <param name="parameterSelector">The function used to convert the value to the parameter in string format.</param>
    /// <returns>The query where it was added to, to chain the calls.</returns>
    public static NameValueCollection AddIfNotNull<T>(
        this NameValueCollection query,
        T? value,
        string key,
        Func<T, string?> parameterSelector)
    {
        if (value is not null)
        {
            query.Add(key, parameterSelector(value));
        }

        return query;
    }
}
