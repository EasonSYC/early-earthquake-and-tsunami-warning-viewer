﻿using System.Diagnostics;
using EasonEetwViewer.KyoshinMonitor.Abstractions;

namespace EasonEetwViewer.KyoshinMonitor.Extensions;
/// <summary>
/// Provides extension methods for converting enums to URI strings.
/// </summary>
internal static class EnumToUriStringExtensions
{
    /// <summary>
    /// Represents the enum in a string that is used in the URI of the kmoni.
    /// </summary>
    /// <param name="kmoniDataType">The current instance of <c>KmoniDataType</c></param>
    /// <returns>A string that is used in the URI of kmoni.</returns>
    public static string ToUriString(this MeasurementType kmoniDataType)
        => kmoniDataType switch
        {
            MeasurementType.MeasuredIntensity
                => "jma",
            MeasurementType.PeakGroundAcceleration
                => "acmap",
            MeasurementType.PeakGroundVelocity
                => "vcmap",
            MeasurementType.PeakGroundDisplacement
                => "dcmap",
            MeasurementType.Response0125
                => "rsp0125",
            MeasurementType.Response0250
                => "rsp0250",
            MeasurementType.Response0500
                => "rsp0500",
            MeasurementType.Response1000
                => "rsp1000",
            MeasurementType.Response2000
                => "rsp2000",
            MeasurementType.Response4000
                => "rsp4000",
            _
                => throw new UnreachableException(),
        };
    /// <summary>
    /// Represents the enum in a string that is used in the URI of the kmoni.
    /// </summary>
    /// <param name="sensorType">The current instance of <c>SensorType</c></param>
    /// <returns>A string that is used in the URI of kmoni.</returns>
    public static string ToUriString(this SensorType sensorType)
        => sensorType switch
        {
            SensorType.Surface
                => "s",
            SensorType.Borehole
                => "b",
            _
                => throw new UnreachableException(),
        };
}