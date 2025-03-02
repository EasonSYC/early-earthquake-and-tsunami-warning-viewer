using System.Diagnostics;
using EasonEetwViewer.Dmdata.Dtos.Enum;

namespace EasonEetwViewer.Dmdata.Telegram.Dtos.EarthquakeInformation.Enum;
public static class IntensityWithUnreceivedExtensions
{
    public static Intensity? ToEarthquakeIntensity(this IntensityWithUnreceived intensity) => intensity switch
    {
        IntensityWithUnreceived.One => Intensity.One,
        IntensityWithUnreceived.Two => Intensity.Two,
        IntensityWithUnreceived.Three => Intensity.Three,
        IntensityWithUnreceived.Four => Intensity.Four,
        IntensityWithUnreceived.FiveWeak => Intensity.FiveWeak,
        IntensityWithUnreceived.FiveStrong => Intensity.FiveStrong,
        IntensityWithUnreceived.SixWeak => Intensity.SixWeak,
        IntensityWithUnreceived.SixStrong => Intensity.SixStrong,
        IntensityWithUnreceived.Seven => Intensity.Seven,
        IntensityWithUnreceived.Unreceived => null,
        _ => throw new UnreachableException()
    };
}
