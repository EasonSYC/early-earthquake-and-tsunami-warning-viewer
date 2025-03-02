using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Services.Logging;
/// <summary>
/// A logger provider that creates <see cref="FileLogger"/> instances.
/// </summary>
/// <param name="logFileWriter">The <see cref="StreamWriter"/> to write logs to.</param>
/// <param name="minimumLogLevel">The minimum log level for the logger.</param>
internal class FileLoggerProvider(StreamWriter logFileWriter, LogLevel minimumLogLevel) : ILoggerProvider
{
    /// <inheritdoc/>
    public ILogger CreateLogger(string categoryName)
        => new FileLogger(categoryName, logFileWriter, minimumLogLevel);
    /// <inheritdoc/>
    public void Dispose()
        => logFileWriter.Dispose();
}
