﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Data.Converters;
using EasonEetwViewer.KyoshinMonitor.Dto.Enum;
using EasonEetwViewer.Models.EnumExtensions;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Converters;

internal class KmoniSensorChoiceConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is SensorType sensorType
            ? sensorType switch
            {
                SensorType.Surface => Resources.KmoniSensorsSurfaceText,
                SensorType.Borehole => Resources.KmoniSensorsBoreholeText,
                _ => null,
            }
            : (object?)null;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
