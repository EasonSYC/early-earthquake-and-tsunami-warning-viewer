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

        Logger.LogDelegate += (level, message, ex) =>
        {
            switch (level)
            {
                case Mapsui.Logging.LogLevel.Trace:
                    logger.LogTrace(ex, "{Message}", message);
                    break;
                case Mapsui.Logging.LogLevel.Information:
                    logger.LogInformation(ex, "{Message}", message);
                    break;
                case Mapsui.Logging.LogLevel.Debug:
                    logger.LogDebug(ex, "{Message}", message);
                    break;
                case Mapsui.Logging.LogLevel.Warning:
                    logger.LogWarning(ex, "{Message}", message);
                    break;
                case Mapsui.Logging.LogLevel.Error:
                    logger.LogError(ex, "{Message}", message);
                    break;
                default:
                    throw new UnreachableException();
            }
        };

        return serviceProvider;
    }
}
