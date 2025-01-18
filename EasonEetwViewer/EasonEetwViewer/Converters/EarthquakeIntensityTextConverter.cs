using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Data.Converters;
using EasonEetwViewer.HttpRequest.Dto.Enum;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Converters;
internal class EarthquakeIntensityTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is EarthquakeIntensity?
            ? value switch
            {
                EarthquakeIntensity.One => Resources.EarthquakeIntensityOneText,
                EarthquakeIntensity.Two => Resources.EarthquakeIntensityTwoText,
                EarthquakeIntensity.Three => Resources.EarthquakeIntensityThreeText,
                EarthquakeIntensity.Four => Resources.EarthquakeIntensityFourText,
                EarthquakeIntensity.FiveWeak => Resources.EarthquakeIntensityFiveWeakText,
                EarthquakeIntensity.FiveStrong => Resources.EarthquakeIntensityFiveStrongText,
                EarthquakeIntensity.SixWeak => Resources.EarthquakeIntensitySixWeakText,
                EarthquakeIntensity.SixStrong => Resources.EarthquakeIntensitySixStrongText,
                EarthquakeIntensity.Seven => Resources.EarthquakeIntensitySevenText,
                EarthquakeIntensity.Unknown or null or _ => Resources.EarthquakeIntensityUnknownText
            }
            : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
