using System.Diagnostics;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Enum;

namespace EasonEetwViewer.Dmdata.Caller.Extensions;
internal static class IntensityExtensions
{
    public static string ToUriString(this Intensity intensity) => intensity switch
    {
        Intensity.One => "1",
        Intensity.Two => "2",
        Intensity.Three => "3",
        Intensity.Four => "4",
        Intensity.FiveWeak => "5-",
        Intensity.FiveStrong => "5+",
        Intensity.SixWeak => "6-",
        Intensity.SixStrong => "6+",
        Intensity.Seven => "7",
        _ => throw new UnreachableException()
    };
}
