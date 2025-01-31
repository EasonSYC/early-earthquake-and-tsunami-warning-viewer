namespace EasonEetwViewer.JmaTravelTime;
public interface ITimeTableProvider
{
    public (double pDistance, double sDistance) DistanceFromDepthTime(int depth, double timeSecond);
}
