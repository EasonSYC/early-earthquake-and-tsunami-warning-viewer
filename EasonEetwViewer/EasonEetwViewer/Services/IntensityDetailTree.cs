using EasonEetwViewer.Dmdata.Dto.ApiResponse.Enum;
using EasonEetwViewer.Models;

namespace EasonEetwViewer.Services;
internal record IntensityDetailTree
{
    internal Dictionary<Intensity, PositionNode> Intensities { get; private set; } = [];

    internal void AddPositionNode(Intensity intensity, PositionNode node)
    {
        if (!Intensities.TryGetValue(intensity, out PositionNode? value))
        {
            value = new PositionNode(string.Empty, string.Empty);
            Intensities.Add(intensity, value);
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
        => Intensities.Keys.Select(i => new DetailIntensityTemplate(i, GetNodeWithFixedIntensity(i)!));
    internal IEnumerable<PositionNode>? GetNodeWithFixedIntensity(Intensity intensity)
        => !Intensities.TryGetValue(intensity, out PositionNode? value) ? null : value.SubNodes;
}
