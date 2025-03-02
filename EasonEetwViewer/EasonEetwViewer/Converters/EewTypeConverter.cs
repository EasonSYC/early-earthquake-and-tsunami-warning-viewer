using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.Lang;
using EasonEetwViewer.Models.RealTimePage;

namespace EasonEetwViewer.Converters;
internal class EewTypeConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is EewDetailsTemplate eew
            ? (RealtimePageResources.RoundBracketsStart
                + (eew.IsCancelled
                    ? RealtimePageResources.EewTextCancelling
                    : eew.IsLastInfo
                        ? RealtimePageResources.EewTextFinal
                        : eew.IsWarning
                            ? RealtimePageResources.EewTextWarning
                            : RealtimePageResources.EewTextForecast)
                + RealtimePageResources.RoundBracketsEnd)
            : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
