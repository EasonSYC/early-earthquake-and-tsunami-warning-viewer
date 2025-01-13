namespace EasonEetwViewer.HttpRequest.Dto.Enum;
internal static class EarthquakeIntensityExtensions
{
    public static string ToUriString(this EarthquakeIntensity intensity) => intensity switch
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
}
