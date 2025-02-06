namespace EasonEetwViewer.KyoshinMonitor.Services;
/// <summary>
/// The options to create an instance of <see cref="ImageFetch"/>.
/// </summary>
public sealed record ImageFetchOptions
{
    /// <summary>
    /// The base URI for the images.
    /// </summary>
    public required string BaseUri { get; set; }
    /// <summary>
    /// The relative URI for the images, with data yet to be filled in.
    /// </summary>
    public required string RelativeUri { get; set; }
}
