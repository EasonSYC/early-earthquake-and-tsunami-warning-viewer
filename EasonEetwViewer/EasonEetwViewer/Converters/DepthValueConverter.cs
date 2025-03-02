using System.Diagnostics;
using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.Dmdata.Dtos.DmdataComponent;
using EasonEetwViewer.Dmdata.Dtos.DmdataComponent.Enum;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Converters;
internal class DepthValueConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is Depth depth
            ? depth.Condition is DepthCondition condition
                ? condition switch
                {
                    DepthCondition.Deep => EarthquakeResources.EarthquakeDepthDeep,
                    DepthCondition.Shallow => EarthquakeResources.EarthquakeDepthShallow,
                    DepthCondition.Unclear => EarthquakeResources.UnknownText,
                    _ => throw new UnreachableException()
                }
                : depth.Value
            : value is null
                ? EarthquakeResources.UnknownText
                : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
