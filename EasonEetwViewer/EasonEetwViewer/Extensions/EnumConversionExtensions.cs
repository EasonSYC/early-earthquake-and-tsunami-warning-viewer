using System.Diagnostics;
using EasonEetwViewer.Dmdata.Dtos.Enum;
using EasonEetwViewer.Dmdata.Telegram.Dtos.EarthquakeInformation.Enum;
using EasonEetwViewer.Dmdata.Telegram.Dtos.EewInformation.Enum.Range;

namespace EasonEetwViewer.Extensions;

internal static class EnumConversionExtensions
{
    public static Intensity? ToIntensity(this IntensityWithUnreceived intensity)
        => intensity switch
        {
            IntensityWithUnreceived.One
                => Intensity.One,
            IntensityWithUnreceived.Two
                => Intensity.Two,
            IntensityWithUnreceived.Three
                => Intensity.Three,
            IntensityWithUnreceived.Four
                => Intensity.Four,
            IntensityWithUnreceived.FiveWeak
                => Intensity.FiveWeak,
            IntensityWithUnreceived.FiveStrong
                => Intensity.FiveStrong,
            IntensityWithUnreceived.SixWeak
                => Intensity.SixWeak,
            IntensityWithUnreceived.SixStrong
                => Intensity.SixStrong,
            IntensityWithUnreceived.Seven
                => Intensity.Seven,
            IntensityWithUnreceived.Unreceived
                => null,
            _
                => throw new UnreachableException()
        };

    public static Intensity? ToIntensity(this IntensityLower intensity)
        => intensity switch
        {
            IntensityLower.One
                => Intensity.One,
            IntensityLower.Two
                => Intensity.Two,
            IntensityLower.Three
                => Intensity.Three,
            IntensityLower.Four
                => Intensity.Four,
            IntensityLower.FiveWeak
                => Intensity.FiveWeak,
            IntensityLower.FiveStrong
                => Intensity.FiveStrong,
            IntensityLower.SixWeak
                => Intensity.SixWeak,
            IntensityLower.SixStrong
                => Intensity.SixStrong,
            IntensityLower.Seven
                => Intensity.Seven,
            IntensityLower.Zero or IntensityLower.Unclear
                => null,
            _
                => throw new UnreachableException()
        };

    public static Intensity? ToIntensity(this IntensityUpper intensity)
        => intensity switch
        {
            IntensityUpper.One
                => Intensity.One,
            IntensityUpper.Two
                => Intensity.Two,
            IntensityUpper.Three
                => Intensity.Three,
            IntensityUpper.Four
                => Intensity.Four,
            IntensityUpper.FiveWeak
                => Intensity.FiveWeak,
            IntensityUpper.FiveStrong
                => Intensity.FiveStrong,
            IntensityUpper.SixWeak
                => Intensity.SixWeak,
            IntensityUpper.SixStrong
                => Intensity.SixStrong,
            IntensityUpper.Seven
                => Intensity.Seven,
            IntensityUpper.Zero or IntensityUpper.Unclear or IntensityUpper.Above
                => null,
            _
                => throw new UnreachableException()
        };
}
