using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.Lang;
using EasonEetwViewer.Models.RealTimePage;

namespace EasonEetwViewer.Converters;
internal class TsunamiTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is TsunamiWarningType warningType
            ? warningType switch
            {
                TsunamiWarningType.Information => RealtimePageResources.TsunamiTextInformation,
                TsunamiWarningType.Caution => RealtimePageResources.TsunamiTextCaution,
                TsunamiWarningType.Warning => RealtimePageResources.TsunamiTextWarning,
                TsunamiWarningType.SpecialWarning => RealtimePageResources.TsunamiTextSpecialWarning,
                TsunamiWarningType.None or _ => RealtimePageResources.TsunamiTextNone,
            }
            : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
