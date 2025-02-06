using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Logging;
internal class FileLoggerProvider(StreamWriter logFileWriter, LogLevel mininumLogLevel) : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName) => new FileLogger(categoryName, logFileWriter, mininumLogLevel);
    public void Dispose() => logFileWriter.Dispose();
}
