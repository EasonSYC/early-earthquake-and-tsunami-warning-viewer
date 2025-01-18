using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.HttpRequest.DmdataComponent;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Converters;
internal class HypocentreConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is Hypocentre hypocentre
            ? hypocentre.Name
            : value is null
                ? Resources.UnknownText
                : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
