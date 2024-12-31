using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasonEetwViewer.KyoshinMonitor.Enum;
public enum SensorType
{
    Unknown = 0,
    Surface = 1,
    Borehole = 2
}

public static class SensorTypeExtensions
{
    public static string ToUriString(this SensorType sensorType)
    {
        return sensorType switch
        {
            SensorType.Surface => "s",
            SensorType.Borehole => "b",
            _ => "unknown",
        };
    }
}