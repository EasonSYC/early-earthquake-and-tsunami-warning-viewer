using EasonEetwViewer.Dtos.Enum;
using EasonEetwViewer.Models.PastPage;
using EasonEetwViewer.Models.RealTimePage;

namespace EasonEetwViewer.Services;
internal record IntensityDetailTree
{
    private readonly Dictionary<Intensity, PositionNode> _intensities = [];

    internal void AddPositionNode(Intensity intensity, PositionNode node)
    {
        if (!_intensities.TryGetValue(intensity, out PositionNode? value))
        {
            value = new PositionNode(string.Empty, string.Empty);
            _intensities.Add(intensity, value);
        }

        value.AddPositionNode(node);
    }

    internal void AddPositionNode(Intensity intensity, IEnumerable<PositionNode> nodes)
    {
        foreach (PositionNode node in nodes)
        {
            AddPositionNode(intensity, node);
        }
    }

    internal void AddPositionNode(IEnumerable<(Intensity, PositionNode)> intensityAndNodes)
    {
        foreach ((Intensity i, PositionNode node) in intensityAndNodes)
        {
            AddPositionNode(i, node);
        }
    }

    internal IEnumerable<DetailIntensityTemplate> ToItemControlDisplay()
        => _intensities.Keys.Select(i => new DetailIntensityTemplate(i, GetNodeWithFixedIntensity(i)!));
    internal IEnumerable<PositionNode>? GetNodeWithFixedIntensity(Intensity intensity)
        => !_intensities.TryGetValue(intensity, out PositionNode? value) ? null : value.SubNodes;
}
