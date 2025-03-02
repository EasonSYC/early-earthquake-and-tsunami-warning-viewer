using EasonEetwViewer.Services.Kmoni.Dtos;
using EasonEetwViewer.Services.Kmoni.Services;

namespace EasonEetwViewer.Services.Kmoni.Options;
/// <summary>
/// Represents the options for the <see cref="KmoniSettingsHelper"/>.
/// </summary>
public sealed record KmoniSettingsHelperOptions
{
    /// <summary>
    /// The filepath to which the setting is stored.
    /// </summary>
    public required string FilePath { get; init; }
    /// <summary>
    /// The default settings for <see cref="KmoniSettings"/>.
    /// </summary>
    public required KmoniSettings Default { get; init; }
}
