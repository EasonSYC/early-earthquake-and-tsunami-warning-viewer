using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Logging;
internal static class DebugLoggerExtensions
{
    public static ILoggingBuilder AddDebugLogger(this ILoggingBuilder builder, LogLevel minimumLogLevel)
    {
        _ = builder.Services.AddSingleton<ILoggerProvider, DebugLoggerProvider>(sp => new(minimumLogLevel));
        return builder;
    }
}
