namespace EasonEetwViewer.JmaTravelTime.Abstractions;
/// <summary>
/// Represents a time table for seismic waves travel time.
/// </summary>
public interface ITimeTable
{
    /// <summary>
    /// Gives the distance that seismic waves travel from the depth of the hypocentre and the time elapsed.
    /// </summary>
    /// <param name="depth">The depth of the hypocentre</param>
    /// <param name="timeSecond">The time elapsed.</param>
    /// <returns>A pair of <see cref="double"/>, representing the distance travelled by the P-wave and S-wave respectively.</returns>
    public (double pDistance, double sDistance) DistanceFromDepthTime(int depth, double timeSecond);
}
