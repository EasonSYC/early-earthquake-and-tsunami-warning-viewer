using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasonEetwViewer.Models.PastPage;

internal record DisplayNode
{
    public required string Name { get; init; }
    public IEnumerable<DisplayNode> SubNodes { get; init; } = [];
}
