using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EasonEetwViewer.KyoshinMonitor.Dto;
/// <summary>
/// Provides extension methods to write a collection of observation points to a specified file path.
/// </summary>
public static class ObservationPointCollectionExtensions
{
    /// <summary>
    /// Writes the list of observation points to the specified file path.
    /// </summary>
    /// <param name="observationpoints">The collection of points.</param>
    /// <param name="filePath">The path to write the file to.</param>
    public static void ToFile(this IEnumerable<ObservationPoint> observationpoints, string filePath)
        => File.WriteAllText(filePath, JsonSerializer.Serialize(observationpoints));
}
