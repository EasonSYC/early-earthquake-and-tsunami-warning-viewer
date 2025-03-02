namespace EasonEetwViewer.Models.RealTimePage;
internal record PositionNode
{
    internal ICollection<PositionNode>? SubNodes { get; private set; }
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
            PositionNode? potentialNode = SubNodes.FirstOrDefault(x => x.Code == newNode.Code);
            if (potentialNode is null)
            {
                SubNodes.Add(newNode);
            }
            else
            {
                if (potentialNode.SubNodes is null)
                {
                    return;
                }

                foreach (PositionNode node in potentialNode.SubNodes)
                {
                    potentialNode.AddPositionNode(node);
                }
            }
        }
    }
}
