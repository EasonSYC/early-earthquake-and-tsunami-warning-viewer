using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Data.Converters;
using EasonEetwViewer.Models;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Converters;
internal class EewTypeConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is EewDetailsTemplate eew
            ? (RealtimePageResources.RoundBracketsStart
                + (eew.IsCancelled
                    ? RealtimePageResources.EewCancellingText
                    : eew.IsLastInfo
                        ? RealtimePageResources.EewFinalText
                        : eew.IsWarning
                            ? RealtimePageResources.EewWarningText
                            : RealtimePageResources.EewForecastText)
                + RealtimePageResources.RoundBracketsEnd)
            : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
