using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasonEetwViewer.Models;
internal record PositionNode
{
    internal List<PositionNode>? SubNodes { get; private set; }
    internal string Title { get; init; }
    internal string Code { get; init; }
    internal PositionNode(string title, string code)
    {
        Title = title;
        Code = code;
    }
    internal PositionNode(string title, string code, PositionNode node)
    {
        Title = title;
        Code = code;
        SubNodes = [node];
    }
    internal void AddPositionNode(PositionNode newNode)
    {
        if (SubNodes is null)
        {
            SubNodes = [newNode];
        }
        else
        {
            List<PositionNode> potentialNodes = SubNodes.Where(x => x.Code == newNode.Code).ToList();
            if (potentialNodes.Count == 0)
            {
                SubNodes.Add(newNode);
            }
            else
            {
                newNode.SubNodes?.ForEach(x => potentialNodes[0].AddPositionNode(x));
            }
        }
    }

}
