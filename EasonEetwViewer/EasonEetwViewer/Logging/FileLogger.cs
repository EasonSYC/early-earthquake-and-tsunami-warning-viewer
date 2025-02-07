using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Logging;
internal class FileLogger(string name, StreamWriter logFileWriter, LogLevel minimumLogLevel) : ILogger
{
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;
    public bool IsEnabled(LogLevel logLevel) => logLevel >= minimumLogLevel;
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (IsEnabled(logLevel))
        {
            string message = formatter(state, exception);
            logFileWriter.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] [{name}] [{logLevel}] : {message}");
            logFileWriter.Flush();
        }
    }
}