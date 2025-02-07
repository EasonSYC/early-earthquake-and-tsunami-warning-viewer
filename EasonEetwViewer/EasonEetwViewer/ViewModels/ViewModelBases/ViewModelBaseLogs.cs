using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.ViewModels.ViewModelBases;
internal static partial class ViewModelBaseLogs
{
    /// <summary>
    /// Log when instantiated.
    /// </summary>
    [LoggerMessage(
        EventId = 0,
        EventName = nameof(Instantiated),
        Level = LogLevel.Information,
        Message = "Instantiated.")]
    public static partial void Instantiated(
        this ILogger<ViewModelBase> logger);
}
