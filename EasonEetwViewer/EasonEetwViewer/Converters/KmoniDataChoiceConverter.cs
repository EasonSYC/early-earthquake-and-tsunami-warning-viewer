using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.KyoshinMonitor.Dto.Enum;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Converters;
internal class KmoniDataChoiceConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is MeasurementType dataType
            ? dataType switch
            {
                MeasurementType.MeasuredIntensity => Resources.KmoniDataIntensity,
                MeasurementType.PeakGroundAcceleration => Resources.KmoniDataPga,
                MeasurementType.PeakGroundVelocity => Resources.KmoniDataPgv,
                MeasurementType.PeakGroundDisplacement => Resources.KmoniDataPgd,
                MeasurementType.Response0125 => Resources.KmoniDataRsp0125,
                MeasurementType.Response0250 => Resources.KmoniDataRsp0250,
                MeasurementType.Response0500 => Resources.KmoniDataRsp0500,
                MeasurementType.Response1000 => Resources.KmoniDataRsp1000,
                MeasurementType.Response2000 => Resources.KmoniDataRsp2000,
                MeasurementType.Response4000 => Resources.KmoniDataRsp4000,
                _ => null,
            }
            : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
