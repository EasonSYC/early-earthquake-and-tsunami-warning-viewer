using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Converters;
internal class WebSocketConnectedConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is bool b
           ? (b ? SettingPageResources.WebSocketConnectedButtonText : SettingPageResources.WebSocketDisconnectedButtonText)
           : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
