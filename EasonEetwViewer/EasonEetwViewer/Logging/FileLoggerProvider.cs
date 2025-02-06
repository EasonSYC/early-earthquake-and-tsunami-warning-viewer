using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Logging;
internal class FileLoggerProvider(StreamWriter logFileWriter) : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName) => new FileLogger(categoryName, logFileWriter);
    public void Dispose() => logFileWriter.Dispose();
}
