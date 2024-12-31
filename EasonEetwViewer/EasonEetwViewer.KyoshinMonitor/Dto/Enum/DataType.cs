using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasonEetwViewer.KyoshinMonitor.Dto.Enum;
public enum KmoniDataType
{
    Unknown = 0,
    MeasuredIntensity = 1,
    PeakGroundAcceleration = 2,
    PeakGroundVelocity = 3,
    PeakGroundDisplacement = 4,
    Response0125 = 5,
    Response0250 = 6,
    Response0500 = 7,
    Response1000 = 8,
    Response2000 = 9,
    Response4000 = 10
}
public static class KmoniDataTypeExtensions
{
    public static string ToUriString(this KmoniDataType kmoniDataType)
    {
        return kmoniDataType switch
        {
            KmoniDataType.MeasuredIntensity => "jma",
            KmoniDataType.PeakGroundAcceleration => "acmap",
            KmoniDataType.PeakGroundVelocity => "vcmap",
            KmoniDataType.PeakGroundDisplacement => "dcmap",
            KmoniDataType.Response0125 => "rsp0125",
            KmoniDataType.Response0250 => "rsp0250",
            KmoniDataType.Response0500 => "rsp0500",
            KmoniDataType.Response1000 => "rsp1000",
            KmoniDataType.Response2000 => "rsp2000",
            KmoniDataType.Response4000 => "rsp4000",
            _ => "unknown",
        };
    }
}