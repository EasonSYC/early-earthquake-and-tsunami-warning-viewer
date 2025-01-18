using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Data.Converters;
using Avalonia.Media;
using EasonEetwViewer.HttpRequest.Dto.Enum;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Converters;
internal class EarthquakeIntensityColourConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is EarthquakeIntensity?
            ? value switch
            {
                EarthquakeIntensity.One => new SolidColorBrush(Color.Parse(Resources.EarthquakeIntensityOneColour)),
                EarthquakeIntensity.Two => new SolidColorBrush(Color.Parse(Resources.EarthquakeIntensityTwoColour)),
                EarthquakeIntensity.Three => new SolidColorBrush(Color.Parse(Resources.EarthquakeIntensityThreeColour)),
                EarthquakeIntensity.Four => new SolidColorBrush(Color.Parse(Resources.EarthquakeIntensityFourColour)),
                EarthquakeIntensity.FiveWeak => new SolidColorBrush(Color.Parse(Resources.EarthquakeIntensityFiveWeakColour)),
                EarthquakeIntensity.FiveStrong => new SolidColorBrush(Color.Parse(Resources.EarthquakeIntensityFiveStrongColour)),
                EarthquakeIntensity.SixWeak => new SolidColorBrush(Color.Parse(Resources.EarthquakeIntensitySixWeakColour)),
                EarthquakeIntensity.SixStrong => new SolidColorBrush(Color.Parse(Resources.EarthquakeIntensitySixStrongColour)),
                EarthquakeIntensity.Seven => new SolidColorBrush(Color.Parse(Resources.EarthquakeIntensitySevenColour)),
                EarthquakeIntensity.Unknown or null or _ => new SolidColorBrush(Color.Parse(Resources.EarthquakeIntensityUnknownColour)),
            }
            : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
