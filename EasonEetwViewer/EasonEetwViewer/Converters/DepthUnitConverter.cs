using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.Dmdata.DmdataComponent;
using EasonEetwViewer.Dmdata.DmdataComponent.Enum;

namespace EasonEetwViewer.Converters;
internal class DepthUnitConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is Hypocentre hypocentre
            ? hypocentre.Depth.Condition is DepthCondition
                ? null
                : hypocentre.Depth.Unit
            : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
