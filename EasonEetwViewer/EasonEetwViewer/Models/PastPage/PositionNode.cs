namespace EasonEetwViewer.Models.PastPage;
internal record PositionNode
{
    public ICollection<PositionNode>? SubNodes { get; private set; }
    public string Title { get; init; }
    public string Code { get; init; }
    public PositionNode(string title, string code)
    {
        Title = title;
        Code = code;
        SubNodes = null; ;
    }
    public PositionNode(string title, string code, PositionNode node)
    {
        Title = title;
        Code = code;
        SubNodes = [node];
    }
    public void AddPositionNode(PositionNode newNode)
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
