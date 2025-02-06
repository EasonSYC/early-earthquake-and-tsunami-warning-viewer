using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Logging;
internal class FileLogger : ILogger
{
    private readonly string _name;
    private readonly StreamWriter _logFileWriter;
    internal FileLogger(string name, StreamWriter logFileWriter)
    {
        _name = name;
        _logFileWriter = logFileWriter;
    }
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;
    public bool IsEnabled(LogLevel logLevel) => logLevel >= LogLevel.Information;
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (IsEnabled(logLevel))
        {
            string message = formatter(state, exception);
            _logFileWriter.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] [{_name}] [{logLevel}] : {message}");
            _logFileWriter.Flush();
        }
    }
}