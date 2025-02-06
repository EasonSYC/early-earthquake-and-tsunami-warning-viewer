using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.EewInformation;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.EewInformation.Enum.Accuracy;

namespace EasonEetwViewer.Converters;
internal class EewOnePointConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is Accuracy accuracy
            ? accuracy.Epicentres[0] == EpicentreDepth.LevelIpf1Plum
                && accuracy.Epicentres[1] == EpicentreDepth.LevelIpf1Plum
                && accuracy.Depth == EpicentreDepth.LevelIpf1Plum
                && accuracy.Magnitude == Magnitude.LevelOrPlum
                && accuracy.MagnitudePoint == MagnitudePoint.OneOrLevelOrPlum
            : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
