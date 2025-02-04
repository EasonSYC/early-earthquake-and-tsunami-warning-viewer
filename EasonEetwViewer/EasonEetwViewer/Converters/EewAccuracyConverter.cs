using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.EewInformation;

namespace EasonEetwViewer.Converters;
internal class EewAccuracyConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is Accuracy accuracy
            ? null
            : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
