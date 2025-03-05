namespace EasonEetwViewer.Models.PastPage;

internal record DisplayNode
{
    public required string Name { get; init; }
    public IEnumerable<DisplayNode> SubNodes { get; init; } = [];
}
