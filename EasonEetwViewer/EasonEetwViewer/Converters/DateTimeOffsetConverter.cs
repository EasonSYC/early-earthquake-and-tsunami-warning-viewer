using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Converters;
internal class DateTimeOffsetConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is null
            ? Resources.UnknownText
            : value is DateTimeOffset dt
                ? parameter switch
                {
                    "verylong" => dt.ToString("yyyy/MM/dd HH:mm:ss"),
                    "long" => dt.ToString("yyyy/MM/dd HH:mm"),
                    "short" => dt.ToString("MM/dd HH:mm"),
                    _ => null
                }
                : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
