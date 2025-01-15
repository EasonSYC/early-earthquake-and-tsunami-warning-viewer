using EasonEetwViewer.HttpRequest.Dto.Enum;
using EasonEetwViewer.HttpRequest.Dto.JsonTelegram;

namespace EasonEetwViewer.Models.EnumExtensions;
internal static class EarthquakeIntensityWithUnreceivedExtensions
{
    public static EarthquakeIntensity ToEarthquakeIntensity(this EarthquakeIntensityWithUnreceived intensity) => intensity switch
    {
        EarthquakeIntensityWithUnreceived.One => EarthquakeIntensity.One,
        EarthquakeIntensityWithUnreceived.Two => EarthquakeIntensity.Two,
        EarthquakeIntensityWithUnreceived.Three => EarthquakeIntensity.Three,
        EarthquakeIntensityWithUnreceived.Four => EarthquakeIntensity.Four,
        EarthquakeIntensityWithUnreceived.FiveWeak => EarthquakeIntensity.FiveWeak,
        EarthquakeIntensityWithUnreceived.FiveStrong => EarthquakeIntensity.FiveStrong,
        EarthquakeIntensityWithUnreceived.SixWeak => EarthquakeIntensity.SixWeak,
        EarthquakeIntensityWithUnreceived.SixStrong => EarthquakeIntensity.SixStrong,
        EarthquakeIntensityWithUnreceived.Seven => EarthquakeIntensity.Seven,
        EarthquakeIntensityWithUnreceived.Unknown or EarthquakeIntensityWithUnreceived.Unreceived or _ => EarthquakeIntensity.Unknown
    };
}
