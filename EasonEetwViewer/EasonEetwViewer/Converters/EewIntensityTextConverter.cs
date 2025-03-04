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
                    IntensityLower.Zero => EarthquakeResources.IntensityTextZero,
                    IntensityLower.One => EarthquakeResources.IntensityTextOne,
                    IntensityLower.Two => EarthquakeResources.IntensityTextTwo,
                    IntensityLower.Three => EarthquakeResources.IntensityTextThree,
                    IntensityLower.Four => EarthquakeResources.IntensityTextFour,
                    IntensityLower.FiveWeak => EarthquakeResources.IntensityTextFiveWeak,
                    IntensityLower.FiveStrong => EarthquakeResources.IntensityTextFiveStrong,
                    IntensityLower.SixWeak => EarthquakeResources.IntensityTextSixWeak,
                    IntensityLower.SixStrong => EarthquakeResources.IntensityTextSixStrong,
                    IntensityLower.Seven => EarthquakeResources.IntensityTextSeven,
                    IntensityLower.Unclear => EarthquakeResources.IntensityTextUnknown,
                    _ => throw new UnreachableException()
                }
                : lu.To switch
                {
                    IntensityUpper.Zero => EarthquakeResources.IntensityTextZero,
                    IntensityUpper.One => EarthquakeResources.IntensityTextOne,
                    IntensityUpper.Two => EarthquakeResources.IntensityTextTwo,
                    IntensityUpper.Three => EarthquakeResources.IntensityTextThree,
                    IntensityUpper.Four => EarthquakeResources.IntensityTextFour,
                    IntensityUpper.FiveWeak => EarthquakeResources.IntensityTextFiveWeak,
                    IntensityUpper.FiveStrong => EarthquakeResources.IntensityTextFiveStrong,
                    IntensityUpper.SixWeak => EarthquakeResources.IntensityTextSixWeak,
                    IntensityUpper.SixStrong => EarthquakeResources.IntensityTextSixStrong,
                    IntensityUpper.Seven => EarthquakeResources.IntensityTextSeven,
                    IntensityUpper.Unclear or IntensityUpper.Above or _ => throw new UnreachableException()
                }
            : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
