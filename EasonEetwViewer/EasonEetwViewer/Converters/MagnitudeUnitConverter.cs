using System.Diagnostics;
using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.Dtos.DmdataComponent;
using EasonEetwViewer.Dtos.DmdataComponent.Enum;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Converters;
internal class MagnitudeUnitConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is Magnitude magnitude
            ? magnitude.Condition is MagnitudeCondition
                ? Resources.EarthquakeMagnitudeUnitDefault
                : magnitude.Unit switch
                {
                    MagnitudeUnit.JmaMagnitude => Resources.EarthquakeMagnitudeUnitJma,
                    MagnitudeUnit.NormalMagnitude => Resources.EarthquakeMagnitudeUnitMoment,
                    _ => throw new UnreachableException()
                }
            : value is null
                ? Resources.EarthquakeMagnitudeUnitDefault
                : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
