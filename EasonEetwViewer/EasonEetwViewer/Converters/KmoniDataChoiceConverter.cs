using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Data.Converters;
using EasonEetwViewer.KyoshinMonitor.Dto.Enum;
using EasonEetwViewer.Models.EnumExtensions;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Converters;
internal class KmoniDataChoiceConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is KmoniDataType dataType
            ? dataType switch
            {
                KmoniDataType.MeasuredIntensity => Resources.KmoniDataIntensity,
                KmoniDataType.PeakGroundAcceleration => Resources.KmoniDataPga,
                KmoniDataType.PeakGroundVelocity => Resources.KmoniDataPgv,
                KmoniDataType.PeakGroundDisplacement => Resources.KmoniDataPgd,
                KmoniDataType.Response0125 => Resources.KmoniDataRsp0125,
                KmoniDataType.Response0250 => Resources.KmoniDataRsp0250,
                KmoniDataType.Response0500 => Resources.KmoniDataRsp0500,
                KmoniDataType.Response1000 => Resources.KmoniDataRsp1000,
                KmoniDataType.Response2000 => Resources.KmoniDataRsp2000,
                KmoniDataType.Response4000 => Resources.KmoniDataRsp4000,
                _ => null,
            }
            : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
