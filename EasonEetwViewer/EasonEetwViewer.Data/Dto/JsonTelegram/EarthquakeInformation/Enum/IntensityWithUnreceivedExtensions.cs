using EasonEetwViewer.HttpRequest.Dto.Enum;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram.EarthquakeInformation.Enum;
public static class IntensityWithUnreceivedExtensions
{
    public static EarthquakeIntensity ToEarthquakeIntensity(this IntensityWithUnreceived intensity) => intensity switch
    {
        IntensityWithUnreceived.One => EarthquakeIntensity.One,
        IntensityWithUnreceived.Two => EarthquakeIntensity.Two,
        IntensityWithUnreceived.Three => EarthquakeIntensity.Three,
        IntensityWithUnreceived.Four => EarthquakeIntensity.Four,
        IntensityWithUnreceived.FiveWeak => EarthquakeIntensity.FiveWeak,
        IntensityWithUnreceived.FiveStrong => EarthquakeIntensity.FiveStrong,
        IntensityWithUnreceived.SixWeak => EarthquakeIntensity.SixWeak,
        IntensityWithUnreceived.SixStrong => EarthquakeIntensity.SixStrong,
        IntensityWithUnreceived.Seven => EarthquakeIntensity.Seven,
        IntensityWithUnreceived.Unknown or IntensityWithUnreceived.Unreceived or _ => EarthquakeIntensity.Unknown
    };
}
