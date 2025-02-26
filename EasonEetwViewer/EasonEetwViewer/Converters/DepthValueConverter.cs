using System.Diagnostics;
using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.Dtos.DmdataComponent;
using EasonEetwViewer.Dtos.DmdataComponent.Enum;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Converters;
internal class DepthValueConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is Depth depth
            ? depth.Condition is DepthCondition condition
                ? condition switch
                {
                    DepthCondition.Deep => Resources.EarthquakeDepthDeep,
                    DepthCondition.Shallow => Resources.EarthquakeDepthShallow,
                    DepthCondition.Unclear => Resources.UnknownText,
                    _ => throw new UnreachableException()
                }
                : depth.Value
            : value is null
                ? Resources.UnknownText
                : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
