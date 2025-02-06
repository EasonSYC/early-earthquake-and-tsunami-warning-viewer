using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Logging;
internal static class FileLoggerExtensions
{
    public static ILoggingBuilder AddFileLogger(this ILoggingBuilder builder, StreamWriter writer, LogLevel minimumLogLEvel)
    {
        _ = builder.Services.AddSingleton<ILoggerProvider, FileLoggerProvider>(sp => new(writer, minimumLogLEvel));
        return builder;
    }
}
