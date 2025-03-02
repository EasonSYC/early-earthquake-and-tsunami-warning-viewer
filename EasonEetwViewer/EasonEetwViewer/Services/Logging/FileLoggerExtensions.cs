using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Services.Logging;
/// <summary>
/// Provides extension methods to add <see cref="FileLogger"/> on a <see cref="ILoggingBuilder"/>.
/// </summary>
internal static class FileLoggerExtensions
{
    /// <summary>
    /// Adds a file logger to the <see cref="ILoggingBuilder"/> that writes logs to a <see cref="StreamWriter"/>.
    /// </summary>
    /// <param name="builder">The <see cref="ILoggingBuilder"/> where the logger is added to.</param>
    /// <param name="logFileWriter">The <see cref="StreamWriter"/> to write logs to.</param>
    /// <param name="minimumLogLevel">The minimum log level for the logger.</param>
    /// <returns>The <see cref="ILoggingBuilder"/> so that calls can be chained.</returns>
    public static ILoggingBuilder AddFileLogger(
        this ILoggingBuilder builder,
        StreamWriter logFileWriter,
        LogLevel minimumLogLevel)
    {
        builder.Services.TryAddEnumerable(
            ServiceDescriptor.Singleton<ILoggerProvider, FileLoggerProvider>(sp
                => new(logFileWriter, minimumLogLevel)));
        return builder;
    }
}
