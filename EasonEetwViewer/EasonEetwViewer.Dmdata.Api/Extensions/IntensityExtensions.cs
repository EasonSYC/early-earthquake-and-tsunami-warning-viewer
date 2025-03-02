using System.Diagnostics;
using EasonEetwViewer.Dmdata.Dtos.Enum;

namespace EasonEetwViewer.Dmdata.Api.Extensions;
/// <summary>
/// Provides extensions for <see cref="Intensity"/> to convert to URI string to be used in API calls.
/// </summary>
internal static class IntensityExtensions
{
    /// <summary>
    /// Converts the <see cref="Intensity"/> to a URI string.
    /// </summary>
    /// <param name="intensity">The intensity to be converted.</param>
    /// <returns>The URI string for the intensity</returns>
    /// <exception cref="UnreachableException">When the code reaches an unreachable state.</exception>
    public static string ToUriString(this Intensity intensity)
        => intensity switch
        {
            Intensity.One
                => "1",
            Intensity.Two
                => "2",
            Intensity.Three
                => "3",
            Intensity.Four
                => "4",
            Intensity.FiveWeak
                => "5-",
            Intensity.FiveStrong
                => "5+",
            Intensity.SixWeak
                => "6-",
            Intensity.SixStrong
                => "6+",
            Intensity.Seven
                => "7",
            _
                => throw new UnreachableException()
        };
}
