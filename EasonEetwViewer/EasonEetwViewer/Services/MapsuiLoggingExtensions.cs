using System.Diagnostics;
using Mapsui;
using Mapsui.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Services;
internal static class MapsuiLoggingExtensions
{
    // https://mapsui.com/documentation/logging.html
    public static IServiceProvider AttachMapsuiLogging(this IServiceProvider serviceProvider)
    {
        ILogger<Map> logger = serviceProvider.GetRequiredService<ILogger<Map>>();

        Logger.LogDelegate += (Mapsui.Logging.LogLevel level, string message, Exception? ex) =>
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
