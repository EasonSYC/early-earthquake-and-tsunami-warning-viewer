using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.EewInformation;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.EewInformation.Enum.Range;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Converters;
internal class EewIntensityTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is FromTo<IntensityLower, IntensityUpper> lu
            ? lu.To is IntensityUpper.Unknown or IntensityUpper.Unclear or IntensityUpper.Above
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
                    IntensityLower.Unknown or IntensityLower.Unclear or IntensityLower or _ => Resources.EarthquakeIntensityUnknownText
                }
                : lu.To switch
                {
                    IntensityUpper.One => Resources.EarthquakeIntensityOneText,
                    IntensityUpper.Two => Resources.EarthquakeIntensityTwoText,
                    IntensityUpper.Three => Resources.EarthquakeIntensityThreeText,
                    IntensityUpper.Four => Resources.EarthquakeIntensityFourText,
                    IntensityUpper.FiveWeak => Resources.EarthquakeIntensityFiveWeakText,
                    IntensityUpper.FiveStrong => Resources.EarthquakeIntensityFiveStrongText,
                    IntensityUpper.SixWeak => Resources.EarthquakeIntensitySixWeakText,
                    IntensityUpper.SixStrong => Resources.EarthquakeIntensitySixStrongText,
                    IntensityUpper.Seven => Resources.EarthquakeIntensitySevenText,
                    IntensityUpper.Unknown or IntensityUpper.Zero or IntensityUpper.Unclear or IntensityUpper.Above or _ => null
                }
            : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
