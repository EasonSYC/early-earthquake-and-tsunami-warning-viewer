namespace EasonEetwViewer.JmaTravelTime.Dtos;

/// <summary>
/// Represents a row in the time table.
/// </summary>
internal record TimeTableEntry
{
    /// <summary>
    /// The distance that the waves travelled.
    /// </summary>
    public required int Radius { get; init; }
    /// <summary>
    /// The depth of the hypocentre.
    /// </summary>
    public required int Depth { get; init; }
    /// <summary>
    /// An array with lenght 2 of times, the time needed for P-Wave and S-Wave to travel to such position.
    /// </summary>
    public required IEnumerable<double> Times { get; init; }
}
