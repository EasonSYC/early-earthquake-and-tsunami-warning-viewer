namespace EasonEetwViewer.Models.PastPage;
/// <summary>
/// Represents a node in the tree of positions.
/// </summary>
internal record DisplayNode
{
    /// <summary>
    /// The name of the node.
    /// </summary>
    public required string Name { get; init; }
    /// <summary>
    /// The subnodes of the node.
    /// </summary>
    public IEnumerable<DisplayNode> SubNodes { get; init; } = [];
}
