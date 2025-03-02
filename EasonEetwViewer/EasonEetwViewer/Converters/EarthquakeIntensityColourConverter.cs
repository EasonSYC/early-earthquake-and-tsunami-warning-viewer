using System.Diagnostics;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using EasonEetwViewer.Dmdata.Dtos.Enum;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Converters;
internal class EarthquakeIntensityColourConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is Intensity?
            ? value switch
            {
                Intensity.One => new SolidColorBrush(Color.Parse(Resources.EarthquakeIntensityOneColour)),
                Intensity.Two => new SolidColorBrush(Color.Parse(Resources.EarthquakeIntensityTwoColour)),
                Intensity.Three => new SolidColorBrush(Color.Parse(Resources.EarthquakeIntensityThreeColour)),
                Intensity.Four => new SolidColorBrush(Color.Parse(Resources.EarthquakeIntensityFourColour)),
                Intensity.FiveWeak => new SolidColorBrush(Color.Parse(Resources.EarthquakeIntensityFiveWeakColour)),
                Intensity.FiveStrong => new SolidColorBrush(Color.Parse(Resources.EarthquakeIntensityFiveStrongColour)),
                Intensity.SixWeak => new SolidColorBrush(Color.Parse(Resources.EarthquakeIntensitySixWeakColour)),
                Intensity.SixStrong => new SolidColorBrush(Color.Parse(Resources.EarthquakeIntensitySixStrongColour)),
                Intensity.Seven => new SolidColorBrush(Color.Parse(Resources.EarthquakeIntensitySevenColour)),
                null => new SolidColorBrush(Color.Parse(Resources.EarthquakeIntensityUnknownColour)),
                _ => throw new UnreachableException()
            }
            : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
