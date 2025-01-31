using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasonEetwViewer.JmaTravelTime;
public interface ITimeTableProvider
{
    public (double pDistance, double sDistance) DistanceFromDepthTime(int depth, double timeSecond);
}
