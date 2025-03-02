using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasonEetwViewer.KyoshinMonitor.Abstractions;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Extensions;

internal static class EnumDisplayTextExtensions
{
    public static string ToDisplayString(this MeasurementType measurementType)
        => measurementType switch
        {
            MeasurementType.MeasuredIntensity
                => SettingPageResources.KmoniMeasurementTextMeasuredIntensity,
            MeasurementType.PeakGroundAcceleration
                => SettingPageResources.KmoniMeasurementTextPeakGroundAcceleration,
            MeasurementType.PeakGroundVelocity
                => SettingPageResources.KmoniMeasurementTextPeakGroundVelocity,
            MeasurementType.PeakGroundDisplacement
                => SettingPageResources.KmoniMeasurementTextPeakGroundDisplacement,
            MeasurementType.Response0125
                => SettingPageResources.KmoniMeasurementTextResponse0125,
            MeasurementType.Response0250
                => SettingPageResources.KmoniMeasurementTextResponse0250,
            MeasurementType.Response0500
                => SettingPageResources.KmoniMeasurementTextResponse0500,
            MeasurementType.Response1000
                => SettingPageResources.KmoniMeasurementTextResponse1000,
            MeasurementType.Response2000
                => SettingPageResources.KmoniMeasurementTextResponse2000,
            MeasurementType.Response4000
                => SettingPageResources.KmoniMeasurementTextResponse4000,
            _
                => throw new UnreachableException(),
        };

    public static string ToDisplayString(this SensorType sensorType)
        => sensorType switch
        {
            SensorType.Surface
                => SettingPageResources.KmoniSensorTextSurface,
            SensorType.Borehole
                => SettingPageResources.KmoniSensorTextBorehole,
            _
                => throw new UnreachableException(),
        };

}
