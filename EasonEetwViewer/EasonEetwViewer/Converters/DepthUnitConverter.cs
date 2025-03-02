using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.Dmdata.Dtos.DmdataComponent;
using EasonEetwViewer.Dmdata.Dtos.DmdataComponent.Enum;

namespace EasonEetwViewer.Converters;
internal class DepthUnitConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is Depth depth
            ? depth.Condition is DepthCondition
                ? null
                : depth.Unit
            : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
