using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EasonEetwViewer.JmaTravelTime;
/// <summary>
/// Provides extensions to inject service of <see cref="ITimeTable"/> with JMA implementation to the service collection.
/// </summary>
public static class JmaTimeTableServiceCollectionExtensions
{
    /// <summary>
    /// Add the JMA implementations of <see cref="ITimeTable"/> to the service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> where the services is to be added.</param>
    /// <returns>The instance of <see cref="IServiceCollection"/> to be chained.</returns>
    public static IServiceCollection AddJmaTimeTable(this IServiceCollection services)
        => services.AddSingleton<ITimeTable>(sp
            => JmaTimeTableBuilder.FromFile(
                sp.GetRequiredService<IOptions<JmaTimeTableOptions>>().Value.FilePath,
                sp.GetRequiredService<ILogger<JmaTimeTable>>()));
}