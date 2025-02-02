using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasonEetwViewer.Services;
internal interface ITimeProvider
{
    public DateTimeOffset DateTimeOffsetNow();
    public DateTimeOffset DateTimeOffsetUtcNow();
    public DateTime DateTimeNow();
    public DateTime DateTimeUtcNow();
}
