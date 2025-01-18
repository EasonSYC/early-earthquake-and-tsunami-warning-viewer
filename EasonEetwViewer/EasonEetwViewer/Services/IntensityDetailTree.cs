using EasonEetwViewer.HttpRequest.Dto.Enum;
using EasonEetwViewer.Models;

namespace EasonEetwViewer.Services;
internal record IntensityDetailTree
{
    internal Dictionary<EarthquakeIntensity, PositionNode> Intensities { get; private set; } = [];

    internal void AddPositionNode(EarthquakeIntensity intensity, PositionNode node)
    {
        if (!Intensities.TryGetValue(intensity, out PositionNode? value))
        {
            value = new PositionNode(string.Empty, string.Empty);
            Intensities.Add(intensity, value);
        }

        value.AddPositionNode(node);
    }
    internal List<DetailIntensityTemplate> ToItemControlDisplay()
    {
        List<DetailIntensityTemplate> finalResult = [];
        foreach (EarthquakeIntensity intensity in Intensities.Keys)
        {
            finalResult.Add(new(intensity, GetNodeWithFixedIntensity(intensity)!));
        }

        return finalResult;
    }
    internal List<PositionNode>? GetNodeWithFixedIntensity(EarthquakeIntensity intensity)
        => !Intensities.TryGetValue(intensity, out PositionNode? value) ? null : value.SubNodes;
}
