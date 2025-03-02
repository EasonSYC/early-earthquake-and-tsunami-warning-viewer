using EasonEetwViewer.Dmdata.Authentication.Abstractions;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.ViewModels.ViewModelBases;
/// <summary>
/// Logs for <see cref="MapViewModelBase"/>.
/// </summary>
internal static partial class MapViewModelBaseLogs
{
    /// <summary>
    /// Log when map initialised.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    [LoggerMessage(
        EventId = 0,
        EventName = nameof(MapInitialised),
        Level = LogLevel.Information,
        Message = "Map initialised.")]
    public static partial void MapInitialised(
        this ILogger<PageViewModelBase> logger);
}
