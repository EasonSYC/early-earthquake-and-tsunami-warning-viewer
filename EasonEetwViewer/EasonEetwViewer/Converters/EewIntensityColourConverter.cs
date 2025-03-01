using System.Diagnostics;
using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.Lang;
using EasonEetwViewer.Telegram.Dtos.EewInformation;
using EasonEetwViewer.Telegram.Dtos.EewInformation.Enum.Range;

namespace EasonEetwViewer.Converters;
internal class EewIntensityColourConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is FromTo<IntensityLower, IntensityUpper> lu
            ? lu.To is IntensityUpper.Unclear or IntensityUpper.Above
                ? lu.From switch
                {
                    IntensityLower.Zero => Resources.EarthquakeIntensityZeroColour,
                    IntensityLower.One => Resources.EarthquakeIntensityOneColour,
                    IntensityLower.Two => Resources.EarthquakeIntensityTwoColour,
                    IntensityLower.Three => Resources.EarthquakeIntensityThreeColour,
                    IntensityLower.Four => Resources.EarthquakeIntensityFourColour,
                    IntensityLower.FiveWeak => Resources.EarthquakeIntensityFiveWeakColour,
                    IntensityLower.FiveStrong => Resources.EarthquakeIntensityFiveStrongColour,
                    IntensityLower.SixWeak => Resources.EarthquakeIntensitySixWeakColour,
                    IntensityLower.SixStrong => Resources.EarthquakeIntensitySixStrongColour,
                    IntensityLower.Seven => Resources.EarthquakeIntensitySevenColour,
                    IntensityLower.Unclear => Resources.EarthquakeIntensityUnknownColour,
                    _ => throw new UnreachableException()
                }
                : lu.To switch
                {
                    IntensityUpper.Zero => Resources.EarthquakeIntensityZeroColour,
                    IntensityUpper.One => Resources.EarthquakeIntensityOneColour,
                    IntensityUpper.Two => Resources.EarthquakeIntensityTwoColour,
                    IntensityUpper.Three => Resources.EarthquakeIntensityThreeColour,
                    IntensityUpper.Four => Resources.EarthquakeIntensityFourColour,
                    IntensityUpper.FiveWeak => Resources.EarthquakeIntensityFiveWeakColour,
                    IntensityUpper.FiveStrong => Resources.EarthquakeIntensityFiveStrongColour,
                    IntensityUpper.SixWeak => Resources.EarthquakeIntensitySixWeakColour,
                    IntensityUpper.SixStrong => Resources.EarthquakeIntensitySixStrongColour,
                    IntensityUpper.Seven => Resources.EarthquakeIntensitySevenColour,
                    IntensityUpper.Unclear or IntensityUpper.Above or _ => throw new UnreachableException()
                }
            : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
