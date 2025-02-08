using EasonEetwViewer.JmaTravelTime.Services;

namespace EasonEetwViewer.JmaTravelTime.Extensions;
/// <summary>
/// The options to create an instance of <see cref="JmaTimeTable"/>.
/// </summary>
public sealed record JmaTimeTableOptions
{
    /// <summary>
    /// The path to the path of the file which stores the time table.
    /// </summary>
    public required string FilePath { get; set; }
}
