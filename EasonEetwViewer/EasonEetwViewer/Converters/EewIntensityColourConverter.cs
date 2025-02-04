using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.EewInformation;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.EewInformation.Enum.Range;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Converters;
internal class EewIntensityColourConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is FromTo<IntensityLower, IntensityUpper> lu
            ? lu.To is IntensityUpper.Unknown or IntensityUpper.Unclear or IntensityUpper.Above
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
                    IntensityLower.Unknown or IntensityLower.Unclear or IntensityLower or _ => Resources.EarthquakeIntensityUnknownColour,
                }
                : lu.To switch
                {
                    IntensityUpper.One => Resources.EarthquakeIntensityOneColour,
                    IntensityUpper.Two => Resources.EarthquakeIntensityTwoColour,
                    IntensityUpper.Three => Resources.EarthquakeIntensityThreeColour,
                    IntensityUpper.Four => Resources.EarthquakeIntensityFourColour,
                    IntensityUpper.FiveWeak => Resources.EarthquakeIntensityFiveWeakColour,
                    IntensityUpper.FiveStrong => Resources.EarthquakeIntensityFiveStrongColour,
                    IntensityUpper.SixWeak => Resources.EarthquakeIntensitySixWeakColour,
                    IntensityUpper.SixStrong => Resources.EarthquakeIntensitySixStrongColour,
                    IntensityUpper.Seven => Resources.EarthquakeIntensitySevenColour,
                    IntensityUpper.Unknown or IntensityUpper.Zero or IntensityUpper.Unclear or IntensityUpper.Above or _ => null
                }
            : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
