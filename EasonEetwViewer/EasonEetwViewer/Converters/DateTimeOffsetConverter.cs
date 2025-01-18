using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Data.Converters;
using EasonEetwViewer.Lang;
using ExCSS;

namespace EasonEetwViewer.Converters;
internal class DateTimeOffsetConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is null
            ? Resources.UnknownText
            : value is DateTimeOffset dt
                ? parameter switch
                {
                    "long" => dt.ToString("yyyy/MM/dd HH:mm"),
                    "short" => dt.ToString("MM/dd HH:mm"),
                    _ => null
                }
                : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
