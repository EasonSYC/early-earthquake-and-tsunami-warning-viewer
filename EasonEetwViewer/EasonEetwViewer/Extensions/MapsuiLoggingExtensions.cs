using System.Diagnostics;
using Mapsui;
using Mapsui.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Extensions;
/// <summary>
/// Provides extensions to attach MapsUI logging to the <see cref="ILogger"/>.
/// </summary>
internal static class MapsuiLoggingExtensions
{
    // https://mapsui.com/documentation/logging.html
    /// <summary>
    /// Attaches MapsUI logging to the <see cref="ILogger"/>.
    /// </summary>
    /// <param name="serviceProvider">The <see cref="ServiceProvider"/> of the application.</param>
    /// <returns>The <see cref="ServiceProvider"/> so that calls can be chained.</returns>
    /// <exception cref="UnreachableException">When the application reaches an unreachable state.</exception>
    public static IServiceProvider AttachMapsuiLogging(this IServiceProvider serviceProvider)
    {
        ILogger<Map> logger = serviceProvider.GetRequiredService<ILogger<Map>>();

        Logger.Settings.LogMapEvents = true;
        Logger.Settings.LogWidgetEvents = true;
        Logger.LogDelegate += (level, message, ex)
            => logger.Log(level.ToMicrosoftLogLevel(), ex, "{Message}", message);

        return serviceProvider;
    }
}
