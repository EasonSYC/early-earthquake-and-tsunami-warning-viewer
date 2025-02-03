using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Data.Converters;
using EasonEetwViewer.Lang;
using EasonEetwViewer.Models;

namespace EasonEetwViewer.Converters;
internal class EewSerialConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is EewDetailsTemplate eew
            ? eew.IsCancelled
                ? null
                : eew.Serial == 0
                    ? null
                    : RealtimePageResources.EewNumberTextBefore
                        + eew.Serial
                        + RealtimePageResources.EewNumberTextAfter
            : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
