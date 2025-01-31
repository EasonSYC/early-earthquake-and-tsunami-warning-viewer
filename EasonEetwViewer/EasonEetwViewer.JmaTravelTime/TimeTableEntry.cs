namespace EasonEetwViewer.JmaTravelTime;

internal record TimeTableEntry
{
    public required int Radius { get; init; }
    public required int Depth { get; init; }
    public required IEnumerable<double> Times { get; init; }
}
