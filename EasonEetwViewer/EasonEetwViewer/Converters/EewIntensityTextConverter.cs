using System.Diagnostics;
using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.Dmdata.Telegram.Dtos.EewInformation;
using EasonEetwViewer.Dmdata.Telegram.Dtos.EewInformation.Enum.Range;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Converters;
internal class EewIntensityTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is FromTo<IntensityLower, IntensityUpper> lu
            ? lu.To is IntensityUpper.Unclear or IntensityUpper.Above
                ? lu.From switch
                {
                    IntensityLower.Zero => Resources.EarthquakeIntensityZeroText,
                    IntensityLower.One => Resources.EarthquakeIntensityOneText,
                    IntensityLower.Two => Resources.EarthquakeIntensityTwoText,
                    IntensityLower.Three => Resources.EarthquakeIntensityThreeText,
                    IntensityLower.Four => Resources.EarthquakeIntensityFourText,
                    IntensityLower.FiveWeak => Resources.EarthquakeIntensityFiveWeakText,
                    IntensityLower.FiveStrong => Resources.EarthquakeIntensityFiveStrongText,
                    IntensityLower.SixWeak => Resources.EarthquakeIntensitySixWeakText,
                    IntensityLower.SixStrong => Resources.EarthquakeIntensitySixStrongText,
                    IntensityLower.Seven => Resources.EarthquakeIntensitySevenText,
                    IntensityLower.Unclear => Resources.EarthquakeIntensityUnknownText,
                    _ => throw new UnreachableException()
                }
                : lu.To switch
                {
                    IntensityUpper.Zero => Resources.EarthquakeIntensityZeroText,
                    IntensityUpper.One => Resources.EarthquakeIntensityOneText,
                    IntensityUpper.Two => Resources.EarthquakeIntensityTwoText,
                    IntensityUpper.Three => Resources.EarthquakeIntensityThreeText,
                    IntensityUpper.Four => Resources.EarthquakeIntensityFourText,
                    IntensityUpper.FiveWeak => Resources.EarthquakeIntensityFiveWeakText,
                    IntensityUpper.FiveStrong => Resources.EarthquakeIntensityFiveStrongText,
                    IntensityUpper.SixWeak => Resources.EarthquakeIntensitySixWeakText,
                    IntensityUpper.SixStrong => Resources.EarthquakeIntensitySixStrongText,
                    IntensityUpper.Seven => Resources.EarthquakeIntensitySevenText,
                    IntensityUpper.Unclear or IntensityUpper.Above or _ => throw new UnreachableException()
                }
            : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
