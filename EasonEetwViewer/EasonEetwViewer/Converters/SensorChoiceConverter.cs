using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.KyoshinMonitor.Abstractions;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Converters;

internal class SensorChoiceConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is SensorType sensorType
            ? sensorType switch
            {
                SensorType.Surface => SettingPageResources.KmoniSensorsSurfaceText,
                SensorType.Borehole => SettingPageResources.KmoniSensorsBoreholeText,
                _ => null,
            }
            : (object?)null;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
