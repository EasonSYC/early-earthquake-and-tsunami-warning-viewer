using Microsoft.Extensions.Logging;
using Avalonia.Logging;
using System.Diagnostics;

namespace EasonEetwViewer.Extensions;

/// <summary>
/// Provides extensions for logging services.
/// </summary>
internal static class LoggingExtensions
{
    /// <summary>
    /// Converts an Avalonia <see cref="LogEventLevel"/> to a Microsoft <see cref="LogLevel"/>.
    /// </summary>
    /// <param name="level">The log event level to be converted.</param>
    /// <returns>The converted log level.</returns>
    /// <exception cref="UnreachableException">When the program reaches a state that was thought to be unreachable.</exception>
    public static LogLevel ToMicrosoftLogLevel(this LogEventLevel level)
        => level switch
        {
            LogEventLevel.Verbose
                => LogLevel.Trace,
            LogEventLevel.Debug
                => LogLevel.Debug,
            LogEventLevel.Information
                => LogLevel.Information,
            LogEventLevel.Warning
                => LogLevel.Warning,
            LogEventLevel.Error
                => LogLevel.Error,
            LogEventLevel.Fatal
                => LogLevel.Critical,
            _
                => throw new UnreachableException()
        };
    /// <summary>
    /// Converts an MapsUI <see cref="Mapsui.Logging.LogLevel"/> to a Microsoft <see cref="LogLevel"/>.
    /// </summary>
    /// <param name="level">The log event level to be converted.</param>
    /// <returns>The converted log level.</returns>
    /// <exception cref="UnreachableException">When the program reaches a state that was thought to be unreachable.</exception>
    public static LogLevel ToMicrosoftLogLevel(this Mapsui.Logging.LogLevel level)
        => level switch
        {
            Mapsui.Logging.LogLevel.Trace
                => LogLevel.Trace,
            Mapsui.Logging.LogLevel.Debug
                => LogLevel.Debug,
            Mapsui.Logging.LogLevel.Information
                => LogLevel.Information,
            Mapsui.Logging.LogLevel.Warning
                => LogLevel.Warning,
            Mapsui.Logging.LogLevel.Error
                => LogLevel.Error,
            _
                => throw new UnreachableException()
        };
}
