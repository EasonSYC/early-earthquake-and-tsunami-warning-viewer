using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.Dtos.DmdataComponent;
using EasonEetwViewer.Dtos.DmdataComponent.Enum;

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
