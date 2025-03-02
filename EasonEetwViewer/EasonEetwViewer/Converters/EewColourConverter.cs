using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.Lang;
using EasonEetwViewer.Models.RealTimePage;

namespace EasonEetwViewer.Converters;
internal class EewColourConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is EewDetailsTemplate eew
            ? eew.IsCancelled
                ? RealtimePageResources.EewColourCancelling
                : eew.IsLastInfo
                    ? RealtimePageResources.EewColourFinal
                    : eew.IsWarning
                        ? RealtimePageResources.EewColourWarning
                        : RealtimePageResources.EewColourForecast
            : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
