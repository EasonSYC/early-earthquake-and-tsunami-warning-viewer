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
                    IntensityLower.Zero => EarthquakeResources.EarthquakeIntensityZeroColour,
                    IntensityLower.One => EarthquakeResources.IntensityOneColour,
                    IntensityLower.Two => EarthquakeResources.IntensityTwoColour,
                    IntensityLower.Three => EarthquakeResources.IntensityThreeColour,
                    IntensityLower.Four => EarthquakeResources.IntensityFourColour,
                    IntensityLower.FiveWeak => EarthquakeResources.IntensityFiveWeakColour,
                    IntensityLower.FiveStrong => EarthquakeResources.IntensityFiveStrongColour,
                    IntensityLower.SixWeak => EarthquakeResources.IntensitySixWeakColour,
                    IntensityLower.SixStrong => EarthquakeResources.IntensitySixStrongColour,
                    IntensityLower.Seven => EarthquakeResources.IntensitySevenColour,
                    IntensityLower.Unclear => EarthquakeResources.EarthquakeIntensityUnknownColour,
                    _ => throw new UnreachableException()
                }
                : lu.To switch
                {
                    IntensityUpper.Zero => EarthquakeResources.EarthquakeIntensityZeroColour,
                    IntensityUpper.One => EarthquakeResources.IntensityOneColour,
                    IntensityUpper.Two => EarthquakeResources.IntensityTwoColour,
                    IntensityUpper.Three => EarthquakeResources.IntensityThreeColour,
                    IntensityUpper.Four => EarthquakeResources.IntensityFourColour,
                    IntensityUpper.FiveWeak => EarthquakeResources.IntensityFiveWeakColour,
                    IntensityUpper.FiveStrong => EarthquakeResources.IntensityFiveStrongColour,
                    IntensityUpper.SixWeak => EarthquakeResources.IntensitySixWeakColour,
                    IntensityUpper.SixStrong => EarthquakeResources.IntensitySixStrongColour,
                    IntensityUpper.Seven => EarthquakeResources.IntensitySevenColour,
                    IntensityUpper.Unclear or IntensityUpper.Above or _ => throw new UnreachableException()
                }
            : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
