using System.Diagnostics;
using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.Dmdata.Dtos.DmdataComponent;
using EasonEetwViewer.Dmdata.Dtos.DmdataComponent.Enum;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Converters;
internal class MagnitudeValueConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is Magnitude magnitude
            ? magnitude.Condition is MagnitudeCondition condition
                ? condition switch
                {
                    MagnitudeCondition.Huge => EarthquakeResources.EarthquakeMagnitudeHuge,
                    MagnitudeCondition.Unclear => EarthquakeResources.UnknownText,
                    _ => throw new UnreachableException()
                }
                : magnitude.Value
            : value is null
                ? EarthquakeResources.UnknownText
                : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
