using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.KyoshinMonitor.Dto.Enum;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Converters;
internal class MeasurementChoiceConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is MeasurementType dataType
            ? dataType switch
            {
                MeasurementType.MeasuredIntensity => SettingPageResources.KmoniDataIntensity,
                MeasurementType.PeakGroundAcceleration => SettingPageResources.KmoniDataPga,
                MeasurementType.PeakGroundVelocity => SettingPageResources.KmoniDataPgv,
                MeasurementType.PeakGroundDisplacement => SettingPageResources.KmoniDataPgd,
                MeasurementType.Response0125 => SettingPageResources.KmoniDataRsp0125,
                MeasurementType.Response0250 => SettingPageResources.KmoniDataRsp0250,
                MeasurementType.Response0500 => SettingPageResources.KmoniDataRsp0500,
                MeasurementType.Response1000 => SettingPageResources.KmoniDataRsp1000,
                MeasurementType.Response2000 => SettingPageResources.KmoniDataRsp2000,
                MeasurementType.Response4000 => SettingPageResources.KmoniDataRsp4000,
                _ => null,
            }
            : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
