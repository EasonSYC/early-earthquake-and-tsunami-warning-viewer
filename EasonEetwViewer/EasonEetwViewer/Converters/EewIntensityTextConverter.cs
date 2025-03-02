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
                    IntensityLower.Zero => EarthquakeResources.EarthquakeIntensityZeroText,
                    IntensityLower.One => EarthquakeResources.IntensityOneText,
                    IntensityLower.Two => EarthquakeResources.IntensityTwoText,
                    IntensityLower.Three => EarthquakeResources.IntensityThreeText,
                    IntensityLower.Four => EarthquakeResources.IntensityFourText,
                    IntensityLower.FiveWeak => EarthquakeResources.IntensityFiveWeakText,
                    IntensityLower.FiveStrong => EarthquakeResources.IntensityFiveStrongText,
                    IntensityLower.SixWeak => EarthquakeResources.IntensitySixWeakText,
                    IntensityLower.SixStrong => EarthquakeResources.IntensitySixStrongText,
                    IntensityLower.Seven => EarthquakeResources.IntensitySevenText,
                    IntensityLower.Unclear => EarthquakeResources.EarthquakeIntensityUnknownText,
                    _ => throw new UnreachableException()
                }
                : lu.To switch
                {
                    IntensityUpper.Zero => EarthquakeResources.EarthquakeIntensityZeroText,
                    IntensityUpper.One => EarthquakeResources.IntensityOneText,
                    IntensityUpper.Two => EarthquakeResources.IntensityTwoText,
                    IntensityUpper.Three => EarthquakeResources.IntensityThreeText,
                    IntensityUpper.Four => EarthquakeResources.IntensityFourText,
                    IntensityUpper.FiveWeak => EarthquakeResources.IntensityFiveWeakText,
                    IntensityUpper.FiveStrong => EarthquakeResources.IntensityFiveStrongText,
                    IntensityUpper.SixWeak => EarthquakeResources.IntensitySixWeakText,
                    IntensityUpper.SixStrong => EarthquakeResources.IntensitySixStrongText,
                    IntensityUpper.Seven => EarthquakeResources.IntensitySevenText,
                    IntensityUpper.Unclear or IntensityUpper.Above or _ => throw new UnreachableException()
                }
            : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
