using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Logging;
internal class DebugLoggerProvider(LogLevel minimumLogLevel) : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName) => new DebugLogger(categoryName, minimumLogLevel);
    public void Dispose() {; }
}
