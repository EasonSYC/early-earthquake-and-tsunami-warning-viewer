using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.Dmdata.Telegram.Dtos.EewInformation;

namespace EasonEetwViewer.Converters;
internal class EewAssumedHypocentreConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is Earthquake earthquake
            ? parameter switch
            {
                "P" => earthquake.Condition is "仮定震源要素",
                "N" => earthquake.Condition is not "仮定震源要素",
                _ => null
            }
            : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
