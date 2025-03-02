using System.Diagnostics;
using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.Dmdata.Dtos.DmdataComponent;
using EasonEetwViewer.Dmdata.Dtos.DmdataComponent.Enum;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Converters;
internal class MagnitudeUnitConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is Magnitude magnitude
            ? magnitude.Condition is MagnitudeCondition
                ? EarthquakeResources.EarthquakeMagnitudeUnitDefault
                : magnitude.Unit switch
                {
                    MagnitudeUnit.JmaMagnitude => EarthquakeResources.EarthquakeMagnitudeUnitJma,
                    MagnitudeUnit.NormalMagnitude => EarthquakeResources.EarthquakeMagnitudeUnitMoment,
                    _ => throw new UnreachableException()
                }
            : value is null
                ? EarthquakeResources.EarthquakeMagnitudeUnitDefault
                : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
