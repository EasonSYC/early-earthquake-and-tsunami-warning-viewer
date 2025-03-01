using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.Models;

namespace EasonEetwViewer.Converters;
internal class StringNameConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is Func<string> f
           ? f()
           : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
