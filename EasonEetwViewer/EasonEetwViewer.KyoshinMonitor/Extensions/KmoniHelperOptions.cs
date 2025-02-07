using EasonEetwViewer.KyoshinMonitor.Services;

namespace EasonEetwViewer.KyoshinMonitor.Extensions;
/// <summary>
/// The options to create an instance of <see cref="ImageFetch"/> and an instance of <see cref="PointExtract"/>.
/// </summary>
public sealed record KmoniHelperOptions
{
    /// <summary>
    /// The base URI for the images.
    /// </summary>
    public required string BaseUri { get; set; }
    /// <summary>
    /// The relative URI for the images, with data yet to be filled in.
    /// </summary>
    public required string RelativeUri { get; set; }
    /// <summary>
    /// The path to the path of the JSON file which stores the observation points.
    /// </summary>
    public required string FilePath { get; set; }
}
