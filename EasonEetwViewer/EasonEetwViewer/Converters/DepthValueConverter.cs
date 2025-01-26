using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.Dmdata.DmdataComponent;
using EasonEetwViewer.Dmdata.DmdataComponent.Enum;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Converters;
internal class DepthValueConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is Hypocentre hypocentre
            ? hypocentre.Depth.Condition is DepthCondition condition
                ? condition switch
                {
                    DepthCondition.Deep => Resources.EarthquakeDepthDeep,
                    DepthCondition.Shallow => Resources.EarthquakeDepthShallow,
                    DepthCondition.Unclear or DepthCondition.Unknown or _ => Resources.UnknownText
                }
                : hypocentre.Depth.Value
            : value is null
                ? Resources.UnknownText
                : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
