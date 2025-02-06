using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Logging;
internal static class FileLoggerExtensions
{
    public static ILoggingBuilder AddFileLogger(this ILoggingBuilder builder, StreamWriter writer)
    {
        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, FileLoggerProvider>(sp => new(writer)));
        return builder;
    }
}
