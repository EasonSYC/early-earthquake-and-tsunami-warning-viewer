using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Logging;
internal class DebugLogger(string name, LogLevel mininumLogLevel) : ILogger
{
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;
    public bool IsEnabled(LogLevel logLevel) => logLevel >= mininumLogLevel;
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (IsEnabled(logLevel))
        {
            string message = formatter(state, exception);
            Debug.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] [{name}] [{logLevel}] : {message}");
        }
    }
}