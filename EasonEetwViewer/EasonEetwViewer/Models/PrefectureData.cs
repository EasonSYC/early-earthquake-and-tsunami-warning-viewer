using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasonEetwViewer.Models;
internal record PrefectureData
{
    internal required string Code { get; init; }
    internal required string Name { get; init; }
}
