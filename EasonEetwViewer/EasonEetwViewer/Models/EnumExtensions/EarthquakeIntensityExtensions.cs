using EasonEetwViewer.HttpRequest.Dto.Enum;

namespace EasonEetwViewer.Models.EnumExtensions;
public static class EarthquakeIntensityExtensions
{
    public static string ToReadableString(this EarthquakeIntensity intensity) => intensity switch
    {
        EarthquakeIntensity.One => "1",
        EarthquakeIntensity.Two => "2",
        EarthquakeIntensity.Three => "3",
        EarthquakeIntensity.Four => "4",
        EarthquakeIntensity.FiveWeak => "5-",
        EarthquakeIntensity.FiveStrong => "5+",
        EarthquakeIntensity.SixWeak => "6-",
        EarthquakeIntensity.SixStrong => "6+",
        EarthquakeIntensity.Seven => "7",
        EarthquakeIntensity.Unknown or _ => "Unknown"
    };

    public static string? ToColourString(this EarthquakeIntensity? intensity) => intensity switch
    {
        EarthquakeIntensity.One => "F2F2FF",
        EarthquakeIntensity.Two => "00AAFF",
        EarthquakeIntensity.Three => "0041FF",
        EarthquakeIntensity.Four => "FAE696",
        EarthquakeIntensity.FiveWeak => "FFE600",
        EarthquakeIntensity.FiveStrong => "FF9900",
        EarthquakeIntensity.SixWeak => "FF2800",
        EarthquakeIntensity.SixStrong => "A50021",
        EarthquakeIntensity.Seven => "B40068",
        EarthquakeIntensity.Unknown or null or _ => null
    };
}
