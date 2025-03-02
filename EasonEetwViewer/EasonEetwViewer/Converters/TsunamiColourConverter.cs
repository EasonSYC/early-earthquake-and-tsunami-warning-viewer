using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.Extensions;
using EasonEetwViewer.Models.RealTimePage;

namespace EasonEetwViewer.Converters;
internal class TsunamiColourConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is TsunamiWarningType warningType
            ? warningType.ToColourString()
            : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
