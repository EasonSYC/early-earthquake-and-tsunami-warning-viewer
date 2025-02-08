using System.Diagnostics;
using EasonEetwViewer.KyoshinMonitor.Abstractions;

namespace EasonEetwViewer.KyoshinMonitor.Extensions;
/// <summary>
/// Provides extension methods for <c>SensorType</c>.
/// </summary>
internal static class SensorTypeExtensions
{
    /// <summary>
    /// Represents the enum in a string that is used in the URI of the kmoni.
    /// </summary>
    /// <param name="sensorType">The current instance of <c>SensorType</c></param>
    /// <returns>A string that is used in the URI of kmoni.</returns>
    public static string ToUriString(this SensorType sensorType) => sensorType switch
    {
        SensorType.Surface => "s",
        SensorType.Borehole => "b",
        _ => throw new UnreachableException(),
    };
}