using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Logging;
internal class FileLoggerProvider(StreamWriter logFileWriter) : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName) => new FileLogger(categoryName, logFileWriter);
    public void Dispose() => logFileWriter.Dispose();
}
