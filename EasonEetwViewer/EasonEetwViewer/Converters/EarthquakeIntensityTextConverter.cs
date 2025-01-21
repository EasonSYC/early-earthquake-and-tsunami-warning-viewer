using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.HttpRequest.Dto.ApiResponse.Enum;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Converters;
internal class EarthquakeIntensityTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is Intensity?
            ? value switch
            {
                Intensity.One => Resources.EarthquakeIntensityOneText,
                Intensity.Two => Resources.EarthquakeIntensityTwoText,
                Intensity.Three => Resources.EarthquakeIntensityThreeText,
                Intensity.Four => Resources.EarthquakeIntensityFourText,
                Intensity.FiveWeak => Resources.EarthquakeIntensityFiveWeakText,
                Intensity.FiveStrong => Resources.EarthquakeIntensityFiveStrongText,
                Intensity.SixWeak => Resources.EarthquakeIntensitySixWeakText,
                Intensity.SixStrong => Resources.EarthquakeIntensitySixStrongText,
                Intensity.Seven => Resources.EarthquakeIntensitySevenText,
                Intensity.Unknown or null or _ => Resources.EarthquakeIntensityUnknownText
            }
            : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
