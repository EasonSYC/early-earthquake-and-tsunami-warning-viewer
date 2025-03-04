using System.Diagnostics;
using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.Dmdata.Telegram.Dtos.EewInformation;
using EasonEetwViewer.Dmdata.Telegram.Dtos.EewInformation.Enum.Range;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Converters;
internal class EewIntensityColourConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is FromTo<IntensityLower, IntensityUpper> lu
            ? lu.To is IntensityUpper.Unclear or IntensityUpper.Above
                ? lu.From switch
                {
                    IntensityLower.Zero => EarthquakeResources.IntensityColourZero,
                    IntensityLower.One => EarthquakeResources.IntensityColourOne,
                    IntensityLower.Two => EarthquakeResources.IntensityColourTwo,
                    IntensityLower.Three => EarthquakeResources.IntensityColourThree,
                    IntensityLower.Four => EarthquakeResources.IntensityColourFour,
                    IntensityLower.FiveWeak => EarthquakeResources.IntensityColourFiveWeak,
                    IntensityLower.FiveStrong => EarthquakeResources.IntensityColourFiveStrong,
                    IntensityLower.SixWeak => EarthquakeResources.IntensityColourSixWeak,
                    IntensityLower.SixStrong => EarthquakeResources.IntensityColourSixStrong,
                    IntensityLower.Seven => EarthquakeResources.IntensityColourSeven,
                    IntensityLower.Unclear => EarthquakeResources.IntensityColourUnknown,
                    _ => throw new UnreachableException()
                }
                : lu.To switch
                {
                    IntensityUpper.Zero => EarthquakeResources.IntensityColourZero,
                    IntensityUpper.One => EarthquakeResources.IntensityColourOne,
                    IntensityUpper.Two => EarthquakeResources.IntensityColourTwo,
                    IntensityUpper.Three => EarthquakeResources.IntensityColourThree,
                    IntensityUpper.Four => EarthquakeResources.IntensityColourFour,
                    IntensityUpper.FiveWeak => EarthquakeResources.IntensityColourFiveWeak,
                    IntensityUpper.FiveStrong => EarthquakeResources.IntensityColourFiveStrong,
                    IntensityUpper.SixWeak => EarthquakeResources.IntensityColourSixWeak,
                    IntensityUpper.SixStrong => EarthquakeResources.IntensityColourSixStrong,
                    IntensityUpper.Seven => EarthquakeResources.IntensityColourSeven,
                    IntensityUpper.Unclear or IntensityUpper.Above or _ => throw new UnreachableException()
                }
            : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
