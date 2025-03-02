using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasonEetwViewer.Services.Kmoni.Dtos;

namespace EasonEetwViewer.Services.Kmoni.Options;
/// <summary>
/// Represents the options for the <see cref="KmoniSettingsHelper"/>.
/// </summary>
public sealed class KmoniSettingsHelperOptions
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
