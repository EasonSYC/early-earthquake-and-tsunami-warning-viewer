using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Services.Logging;
/// <summary>
/// A logger that writes log messages to a stream, usually a file.
/// </summary>
/// <param name="name">The name of the logger.</param>
/// <param name="logFileWriter">The <see cref="StreamWriter"/> to write logs to.</param>
/// <param name="minimumLogLevel">The minimum log level for the logger.</param>
internal class FileLogger
    (string name,
    StreamWriter
    logFileWriter,
    LogLevel minimumLogLevel) : ILogger
{
    /// <inheritdoc/>
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        => null;
    /// <inheritdoc/>
    public bool IsEnabled(LogLevel logLevel)
        => logLevel >= minimumLogLevel;
    /// <inheritdoc/>
    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (IsEnabled(logLevel))
        {
            logFileWriter.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] [{name}] [{logLevel}] : {formatter(state, exception)}");
            logFileWriter.Flush();
        }
    }
}